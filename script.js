const canvas = document.getElementById('manta-video');
const ctx = canvas.getContext('2d');

const fishColors = ['#c3e8ff', '#8dd5ff', '#9af2d4', '#ffd885'];
const fish = Array.from({ length: 28 }, (_, i) => ({
  x: Math.random() * canvas.width,
  y: 120 + Math.random() * 360,
  speed: 0.4 + Math.random() * 1.2,
  size: 4 + Math.random() * 10,
  color: fishColors[i % fishColors.length],
}));

const miniDrones = Array.from({ length: 6 }, (_, i) => ({
  phase: i * 0.65,
  releasedAt: 1 + i * 0.75,
}));

function drawFish(t) {
  fish.forEach((f) => {
    f.x -= f.speed;
    if (f.x < -40) f.x = canvas.width + 30;

    const wiggle = Math.sin(t * 0.003 + f.y * 0.03) * 2;
    ctx.fillStyle = f.color;
    ctx.beginPath();
    ctx.ellipse(f.x, f.y + wiggle, f.size * 1.3, f.size * 0.65, 0, 0, Math.PI * 2);
    ctx.fill();

    ctx.beginPath();
    ctx.moveTo(f.x + f.size, f.y + wiggle);
    ctx.lineTo(f.x + f.size + 8, f.y + wiggle - 4);
    ctx.lineTo(f.x + f.size + 8, f.y + wiggle + 4);
    ctx.closePath();
    ctx.fill();
  });
}

function drawMotherDrone(t) {
  const x = canvas.width * 0.52 + Math.sin(t * 0.001) * 14;
  const y = canvas.height * 0.35 + Math.cos(t * 0.0012) * 6;

  ctx.save();
  ctx.translate(x, y);
  ctx.fillStyle = '#172f46';
  ctx.beginPath();
  ctx.moveTo(-170, 0);
  ctx.quadraticCurveTo(-30, -90, 0, -65);
  ctx.quadraticCurveTo(30, -90, 170, 0);
  ctx.quadraticCurveTo(20, 85, 0, 70);
  ctx.quadraticCurveTo(-20, 85, -170, 0);
  ctx.closePath();
  ctx.fill();

  ctx.fillStyle = '#2bd7ff';
  ctx.beginPath();
  ctx.arc(0, -8, 11, 0, Math.PI * 2);
  ctx.fill();

  ctx.fillStyle = '#0f2030';
  ctx.fillRect(-28, 48, 56, 14);
  ctx.restore();

  return { x, y };
}

function drawMiniDrones(t, mother) {
  miniDrones.forEach((d, i) => {
    const elapsed = t * 0.001;
    if (elapsed < d.releasedAt) return;

    const local = elapsed - d.releasedAt;
    const mx = mother.x + Math.cos(local + d.phase) * (40 + i * 10);
    const my = mother.y + 60 + local * 38 + Math.sin(local * 2 + i) * 8;

    ctx.fillStyle = '#ff9f50';
    ctx.beginPath();
    ctx.arc(mx, my, 6, 0, Math.PI * 2);
    ctx.fill();

    ctx.strokeStyle = 'rgba(255, 159, 80, 0.5)';
    ctx.beginPath();
    ctx.moveTo(mx, my - 6);
    ctx.lineTo(mx, my - 20);
    ctx.stroke();
  });
}

function drawBackground(t) {
  const gradient = ctx.createLinearGradient(0, 0, 0, canvas.height);
  gradient.addColorStop(0, '#004f86');
  gradient.addColorStop(1, '#011a2c');
  ctx.fillStyle = gradient;
  ctx.fillRect(0, 0, canvas.width, canvas.height);

  for (let i = 0; i < 90; i++) {
    const bx = (i * 97 + t * 0.02) % canvas.width;
    const by = (i * 53) % canvas.height;
    ctx.fillStyle = `rgba(169, 238, 255, ${0.05 + (i % 6) * 0.01})`;
    ctx.fillRect(bx, by, 2, 2);
  }
}

function animate(t) {
  drawBackground(t);
  drawFish(t);
  const mother = drawMotherDrone(t);
  drawMiniDrones(t, mother);

  ctx.fillStyle = 'rgba(224,247,255,0.9)';
  ctx.font = '600 18px Segoe UI';
  ctx.fillText('HARPA Manta - Operação de Enxame Autônomo', 22, 36);

  requestAnimationFrame(animate);
}

requestAnimationFrame(animate);
