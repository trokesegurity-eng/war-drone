export const clamp = (value, min, max) => Math.max(min, Math.min(max, value));

export function moveToward(entity, target, dt, speed, bounds) {
  const dx = target.x - entity.x;
  const dy = target.y - entity.y;
  const mag = Math.hypot(dx, dy) || 1;
  const vx = (dx / mag) * speed;
  const vy = (dy / mag) * speed;
  return {
    x: clamp(entity.x + vx * dt, 28, bounds.width - 28),
    y: clamp(entity.y + vy * dt, 28, bounds.height - 28),
    vx,
    vy,
  };
}

export function hit(a, b) {
  return Math.hypot(a.x - b.x, a.y - b.y) <= a.r + b.r;
}

export function spawnInRing(cx, cy, radiusMin, radiusMax) {
  const angle = Math.random() * Math.PI * 2;
  const radius = radiusMin + Math.random() * (radiusMax - radiusMin);
  return {
    x: cx + Math.cos(angle) * radius,
    y: cy + Math.sin(angle) * radius,
  };
}

export function difficulty(stage) {
  return {
    enemies: 3 + stage,
    nodes: 5 + stage,
    energyDrain: 4 + stage * 0.55,
    enemySpeed: 58 + stage * 12,
  };
}
