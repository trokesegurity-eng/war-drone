import test from 'node:test';
import assert from 'node:assert/strict';
import { clamp, difficulty, hit, moveToward } from '../src/core.js';

test('clamp mantém valor entre limites', () => {
  assert.equal(clamp(14, 0, 10), 10);
  assert.equal(clamp(-2, 0, 10), 0);
  assert.equal(clamp(7, 0, 10), 7);
});

test('hit detecta colisão circular', () => {
  assert.equal(hit({ x: 0, y: 0, r: 10 }, { x: 18, y: 0, r: 8 }), true);
  assert.equal(hit({ x: 0, y: 0, r: 10 }, { x: 40, y: 0, r: 8 }), false);
});

test('moveToward move objeto com limites', () => {
  const next = moveToward({ x: 20, y: 20 }, { x: 200, y: 80 }, 1, 100, { width: 300, height: 200 });
  assert.ok(next.x > 20);
  assert.ok(next.y > 20);
  assert.ok(next.x <= 272);
  assert.ok(next.y <= 172);
});

test('difficulty cresce por estágio', () => {
  const s1 = difficulty(1);
  const s5 = difficulty(5);
  assert.ok(s5.enemies > s1.enemies);
  assert.ok(s5.enemySpeed > s1.enemySpeed);
  assert.ok(s5.energyDrain > s1.energyDrain);
});
