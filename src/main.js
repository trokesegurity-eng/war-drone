import { clamp, difficulty, hit, moveToward, spawnInRing } from './core.js';

const canvas = document.getElementById('game');
const ctx = canvas.getContext('2d');
const scoreEl = document.getElementById('score');
const energyEl = document.getElementById('energy');
const stageEl = document.getElementById('stage');
const restartBtn = document.getElementById('restart');
const pulseBtn = document.getElementById('pulse');
const boostBtn = document.getElementById('boost');

let state;
const keys = new Set();
let touchPoint;
let boostActive = false;

function resize() {
  const dpr = window.devicePixelRatio || 1;
  const rect = canvas.getBoundingClientRect();
  canvas.width = Math.floor(rect.width * dpr);
  canvas.height = Math.floor(rect.height * dpr);
  ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
}

function init() {
  state = {
    t: 0,
    stage: 1,
    score: 0,
    energy: 100,
    over: false,
    player: { x: 220, y: 220, r: 22, speed: 180, sonarCd: 0, pulse: 0, vx: 0, vy: 0 },
    enemies: [],
    nodes: [],
    particles: [],
  };
  fillSector();
  restartBtn.hidden = true;
}

function fillSector() {
  const d = difficulty(state.stage);
  state.nodes = Array.from({ length: d.nodes }, () => ({
    ...spawnInRing(canvas.clientWidth / 2, canvas.clientHeight / 2, 40, Math.min(canvas.clientWidth, canvas.clientHeight) * 0.48),
    r: 11,
    glow: Math.random() * 10,
  }));
  state.enemies = Array.from({ length: d.enemies }, () => ({
    x: Math.random() * canvas.clientWidth,
    y: Math.random() * canvas.clientHeight,
    r: 18,
    speed: d.enemySpeed + Math.random() * 30,
  }));
}

function emit(x, y, color, amount = 8) {
  for (let i = 0; i < amount; i++) {
    const a = Math.random() * Math.PI * 2;
    state.particles.push({
      x,
      y,
      vx: Math.cos(a) * (40 + Math.random() * 90),
      vy: Math.sin(a) * (40 + Math.random() * 90),
      life: 0.35 + Math.random() * 0.4,
      color,
    });
  }
}

function update(dt) {
  if (state.over) return;

  const d = difficulty(state.stage);
  state.t += dt;
  state.energy = clamp(state.energy - d.energyDrain * dt, 0, 100);
  state.player.sonarCd = Math.max(0, state.player.sonarCd - dt);
  state.player.pulse = Math.max(0, state.player.pulse - dt * 1.7);

  const input = { x: 0, y: 0 };
  if (keys.has('arrowup') || keys.has('w')) input.y -= 1;
  if (keys.has('arrowdown') || keys.has('s')) input.y += 1;
  if (keys.has('arrowleft') || keys.has('a')) input.x -= 1;
  if (keys.has('arrowright') || keys.has('d')) input.x += 1;

  if (touchPoint) {
    input.x = touchPoint.dx;
    input.y = touchPoint.dy;
  }

  const mag = Math.hypot(input.x, input.y) || 1;
  const speed = state.player.speed * (boostActive && state.energy > 5 ? 1.55 : 1);
  if (boostActive) state.energy = clamp(state.energy - 15 * dt, 0, 100);
  state.player.vx = (input.x / mag) * speed;
  state.player.vy = (input.y / mag) * speed;
  state.player.x = clamp(state.player.x + state.player.vx * dt, 24, canvas.clientWidth - 24);
  state.player.y = clamp(state.player.y + state.player.vy * dt, 24, canvas.clientHeight - 24);

  state.enemies = state.enemies.map((e) => {
    const moved = moveToward(e, state.player, dt, e.speed, { width: canvas.clientWidth, height: canvas.clientHeight });
    const enemy = { ...e, ...moved };
    if (state.player.pulse > 0 && Math.hypot(enemy.x - state.player.x, enemy.y - state.player.y) < 140) {
      enemy.x += (enemy.x - state.player.x) * 0.09;
      enemy.y += (enemy.y - state.player.y) * 0.09;
    }
    if (hit(enemy, state.player)) {
      state.energy = clamp(state.energy - 35 * dt, 0, 100);
      emit(state.player.x, state.player.y, '#ff4f78', 2);
    }
    return enemy;
  });

  state.nodes = state.nodes.filter((node) => {
    node.glow += dt * 6;
    if (hit(node, state.player)) {
      state.score += 100;
      state.energy = clamp(state.energy + 11, 0, 100);
      emit(node.x, node.y, '#67ffd3', 12);
      return false;
    }
    return true;
  });

  state.particles = state.particles.filter((p) => {
    p.life -= dt;
    p.x += p.vx * dt;
    p.y += p.vy * dt;
    return p.life > 0;
  });

  if (state.nodes.length === 0) {
    state.stage += 1;
    state.score += 400;
    fillSector();
  }

  if (state.energy <= 0) {
    state.over = true;
    restartBtn.hidden = false;
  }
}

function drawGrid() {
  const step = 42;
  ctx.save();
  ctx.strokeStyle = 'rgba(106, 255, 250, 0.08)';
  for (let x = 0; x < canvas.clientWidth; x += step) {
    ctx.beginPath();
    ctx.moveTo(x, 0);
    ctx.lineTo(x, canvas.clientHeight);
    ctx.stroke();
  }
  for (let y = 0; y < canvas.clientHeight; y += step) {
    ctx.beginPath();
    ctx.moveTo(0, y);
    ctx.lineTo(canvas.clientWidth, y);
    ctx.stroke();
  }
  ctx.restore();
}

function drawEntityCircle(x, y, r, color, glow) {
  ctx.save();
  ctx.shadowColor = glow;
  ctx.shadowBlur = 18;
  ctx.fillStyle = color;
  ctx.beginPath();
  ctx.arc(x, y, r, 0, Math.PI * 2);
  ctx.fill();
  ctx.restore();
}

function render() {
  ctx.clearRect(0, 0, canvas.clientWidth, canvas.clientHeight);
  drawGrid();

  state.nodes.forEach((n) => {
    const alpha = 0.5 + 0.3 * Math.sin(n.glow);
    drawEntityCircle(n.x, n.y, n.r, `rgba(114, 255, 226, ${alpha})`, '#67ffd3');
  });

  state.enemies.forEach((e) => drawEntityCircle(e.x, e.y, e.r, 'rgba(255, 70, 132, 0.68)', '#ff5890'));
  drawEntityCircle(state.player.x, state.player.y, state.player.r, 'rgba(57, 187, 255, 0.98)', '#47e4ff');

  if (state.player.pulse > 0) {
    ctx.strokeStyle = `rgba(134,255,247,${state.player.pulse})`;
    ctx.lineWidth = 3;
    ctx.beginPath();
    ctx.arc(state.player.x, state.player.y, 140 * (1.2 - state.player.pulse), 0, Math.PI * 2);
    ctx.stroke();
  }

  state.particles.forEach((p) => {
    ctx.fillStyle = p.color;
    ctx.globalAlpha = p.life;
    ctx.fillRect(p.x, p.y, 3, 3);
    ctx.globalAlpha = 1;
  });

  if (state.over) {
    ctx.fillStyle = 'rgba(3, 7, 14, 0.75)';
    ctx.fillRect(0, 0, canvas.clientWidth, canvas.clientHeight);
    ctx.fillStyle = '#dbf7ff';
    ctx.font = '700 38px Inter, sans-serif';
    ctx.fillText('Missão perdida', canvas.clientWidth / 2 - 130, canvas.clientHeight / 2 - 10);
    ctx.font = '500 22px Inter, sans-serif';
    ctx.fillText(`Score final: ${state.score}`, canvas.clientWidth / 2 - 90, canvas.clientHeight / 2 + 32);
  }

  scoreEl.textContent = `Score: ${state.score}`;
  energyEl.textContent = `Energia: ${Math.round(state.energy)}%`;
  stageEl.textContent = `Setor: ${state.stage}`;
}

let last = performance.now();
function frame(now) {
  const dt = Math.min((now - last) / 1000, 0.033);
  last = now;
  update(dt);
  render();
  requestAnimationFrame(frame);
}

function sonarPulse() {
  if (state.player.sonarCd > 0 || state.over) return;
  state.player.pulse = 1;
  state.player.sonarCd = 1.3;
  emit(state.player.x, state.player.y, '#89ffff', 22);
}

window.addEventListener('keydown', (event) => {
  keys.add(event.key.toLowerCase());
  if (event.code === 'Space') sonarPulse();
});
window.addEventListener('keyup', (event) => keys.delete(event.key.toLowerCase()));
window.addEventListener('resize', resize);

canvas.addEventListener('pointerdown', (event) => {
  const rect = canvas.getBoundingClientRect();
  touchPoint = { dx: (event.clientX - rect.left - state.player.x) / 60, dy: (event.clientY - rect.top - state.player.y) / 60 };
});
canvas.addEventListener('pointermove', (event) => {
  if (!touchPoint) return;
  const rect = canvas.getBoundingClientRect();
  touchPoint.dx = (event.clientX - rect.left - state.player.x) / 60;
  touchPoint.dy = (event.clientY - rect.top - state.player.y) / 60;
});
canvas.addEventListener('pointerup', () => { touchPoint = undefined; });
canvas.addEventListener('pointercancel', () => { touchPoint = undefined; });

boostBtn.addEventListener('pointerdown', () => { boostActive = true; });
boostBtn.addEventListener('pointerup', () => { boostActive = false; });
boostBtn.addEventListener('pointerleave', () => { boostActive = false; });
pulseBtn.addEventListener('click', sonarPulse);

restartBtn.addEventListener('click', init);

resize();
init();
requestAnimationFrame(frame);
