import * as THREE from "three";

const canvas = document.querySelector("#gameCanvas");
const scoreEl = document.querySelector("#score");
const timeEl = document.querySelector("#time");
const healthEl = document.querySelector("#health");
const messageEl = document.querySelector("#message");
const restartBtn = document.querySelector("#restart");

const renderer = new THREE.WebGLRenderer({ canvas, antialias: true });
renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
renderer.setSize(canvas.clientWidth, canvas.clientHeight, false);

const scene = new THREE.Scene();
scene.fog = new THREE.Fog(0x043049, 10, 80);

const camera = new THREE.PerspectiveCamera(60, 1, 0.1, 120);
camera.position.set(0, 8, 26);

scene.add(new THREE.AmbientLight(0x8bcfff, 0.6));
const dirLight = new THREE.DirectionalLight(0xffffff, 1.2);
dirLight.position.set(20, 30, 15);
scene.add(dirLight);

const water = new THREE.Mesh(
  new THREE.SphereGeometry(70, 32, 32),
  new THREE.MeshStandardMaterial({
    color: 0x063754,
    side: THREE.BackSide,
    roughness: 0.7,
    metalness: 0.05
  })
);
scene.add(water);

const floor = new THREE.Mesh(
  new THREE.CircleGeometry(36, 64),
  new THREE.MeshStandardMaterial({ color: 0x3a6d3a, roughness: 1 })
);
floor.rotation.x = -Math.PI / 2;
floor.position.y = -5;
scene.add(floor);

const vegetation = new THREE.Group();
for (let i = 0; i < 35; i += 1) {
  const plant = new THREE.Mesh(
    new THREE.CylinderGeometry(0.1, 0.15, 1.8 + Math.random() * 3, 6),
    new THREE.MeshStandardMaterial({ color: 0x2aa44f, roughness: 0.9 })
  );
  plant.position.set((Math.random() - 0.5) * 60, -4, (Math.random() - 0.5) * 60);
  plant.rotation.z = (Math.random() - 0.5) * 0.4;
  vegetation.add(plant);
}
scene.add(vegetation);

const pirarucuGeometry = new THREE.CapsuleGeometry(0.6, 2.8, 8, 16);
const localFishGeometry = new THREE.CapsuleGeometry(0.35, 1.7, 8, 12);

function createFish(material, geometry, speedRange) {
  const mesh = new THREE.Mesh(geometry, material);
  mesh.position.set((Math.random() - 0.5) * 28, -1 + Math.random() * 8, (Math.random() - 0.5) * 28);
  mesh.rotation.z = Math.PI / 2;

  const velocity = new THREE.Vector3(
    (Math.random() - 0.5) * speedRange,
    (Math.random() - 0.5) * (speedRange * 0.25),
    (Math.random() - 0.5) * speedRange
  );

  return { mesh, velocity };
}

let pirarucus = [];
let nativeFish = [];
let score = 0;
let ecosystemHealth = 100;
let remainingTime = 90;
let gameOver = false;

const raycaster = new THREE.Raycaster();
const pointer = new THREE.Vector2();

const orbit = {
  yaw: 0,
  pitch: -0.15,
  dragging: false,
  x: 0,
  y: 0
};

function setupEntities() {
  pirarucus.forEach((fish) => scene.remove(fish.mesh));
  nativeFish.forEach((fish) => scene.remove(fish.mesh));
  pirarucus = [];
  nativeFish = [];

  const pirarucuMaterial = new THREE.MeshStandardMaterial({ color: 0xff4747, roughness: 0.55, metalness: 0.1 });
  const nativeMaterial = new THREE.MeshStandardMaterial({ color: 0x66b8ff, roughness: 0.7, metalness: 0.08 });

  for (let i = 0; i < 12; i += 1) {
    const fish = createFish(pirarucuMaterial, pirarucuGeometry, 0.055 + Math.random() * 0.045);
    fish.mesh.userData.type = "pirarucu";
    scene.add(fish.mesh);
    pirarucus.push(fish);
  }

  for (let i = 0; i < 16; i += 1) {
    const fish = createFish(nativeMaterial, localFishGeometry, 0.03 + Math.random() * 0.03);
    fish.mesh.userData.type = "nativo";
    scene.add(fish.mesh);
    nativeFish.push(fish);
  }
}

function resetGame() {
  score = 0;
  ecosystemHealth = 100;
  remainingTime = 90;
  gameOver = false;
  restartBtn.hidden = true;
  messageEl.classList.remove("alert");
  messageEl.textContent = "Capture os pirarucus invasores e preserve os peixes nativos.";
  setupEntities();
  syncHud();
}

function syncHud() {
  scoreEl.textContent = String(score);
  timeEl.textContent = String(Math.max(0, Math.ceil(remainingTime)));
  healthEl.textContent = String(Math.max(0, Math.round(ecosystemHealth)));
}

function updateFish(fishArray, bounds, isInvasive) {
  fishArray.forEach((fish) => {
    fish.mesh.position.add(fish.velocity);

    if (Math.abs(fish.mesh.position.x) > bounds) fish.velocity.x *= -1;
    if (fish.mesh.position.y > 9 || fish.mesh.position.y < -3.5) fish.velocity.y *= -1;
    if (Math.abs(fish.mesh.position.z) > bounds) fish.velocity.z *= -1;

    fish.mesh.lookAt(fish.mesh.position.clone().add(fish.velocity));
    fish.mesh.rotateY(Math.PI / 2);

    if (isInvasive) {
      ecosystemHealth -= 0.0045;
    }
  });
}

function checkGameState() {
  if (gameOver) return;

  if (remainingTime <= 0 || ecosystemHealth <= 0 || pirarucus.length === 0) {
    gameOver = true;
    restartBtn.hidden = false;

    if (pirarucus.length === 0) {
      messageEl.textContent = "Parabéns! Você removeu todos os pirarucus e protegeu o ecossistema local.";
    } else if (ecosystemHealth <= 0) {
      messageEl.textContent = "O ecossistema colapsou. Muitos pirarucus invasores permaneceram.";
      messageEl.classList.add("alert");
    } else {
      messageEl.textContent = "Tempo esgotado! Continue estudando manejo de espécies fora do habitat natural.";
      messageEl.classList.add("alert");
    }
  }
}

function onPointerDown(event) {
  orbit.dragging = true;
  orbit.x = event.clientX ?? event.touches?.[0]?.clientX ?? 0;
  orbit.y = event.clientY ?? event.touches?.[0]?.clientY ?? 0;
}

function onPointerMove(event) {
  if (!orbit.dragging) return;
  const x = event.clientX ?? event.touches?.[0]?.clientX ?? orbit.x;
  const y = event.clientY ?? event.touches?.[0]?.clientY ?? orbit.y;

  orbit.yaw -= (x - orbit.x) * 0.003;
  orbit.pitch -= (y - orbit.y) * 0.003;
  orbit.pitch = THREE.MathUtils.clamp(orbit.pitch, -0.75, 0.65);

  orbit.x = x;
  orbit.y = y;
}

function onPointerUp(event) {
  orbit.dragging = false;
  if (gameOver) return;

  const rect = canvas.getBoundingClientRect();
  const x = event.clientX ?? event.changedTouches?.[0]?.clientX;
  const y = event.clientY ?? event.changedTouches?.[0]?.clientY;
  if (x === undefined || y === undefined) return;

  pointer.x = ((x - rect.left) / rect.width) * 2 - 1;
  pointer.y = -((y - rect.top) / rect.height) * 2 + 1;

  raycaster.setFromCamera(pointer, camera);
  const hits = raycaster.intersectObjects(pirarucus.map((fish) => fish.mesh));

  if (hits.length > 0) {
    const mesh = hits[0].object;
    const index = pirarucus.findIndex((fish) => fish.mesh === mesh);
    if (index !== -1) {
      const [captured] = pirarucus.splice(index, 1);
      scene.remove(captured.mesh);
      score += 1;
      ecosystemHealth = Math.min(100, ecosystemHealth + 5);
      messageEl.textContent = "Boa captura! Remover invasores ajuda a recuperar o equilíbrio ecológico.";
      messageEl.classList.remove("alert");
      syncHud();
      checkGameState();
    }
  }
}

function resize() {
  const { clientWidth, clientHeight } = canvas;
  renderer.setSize(clientWidth, clientHeight, false);
  camera.aspect = clientWidth / clientHeight;
  camera.updateProjectionMatrix();
}

window.addEventListener("resize", resize);
canvas.addEventListener("mousedown", onPointerDown);
canvas.addEventListener("mousemove", onPointerMove);
window.addEventListener("mouseup", onPointerUp);
canvas.addEventListener("touchstart", onPointerDown, { passive: true });
canvas.addEventListener("touchmove", onPointerMove, { passive: true });
canvas.addEventListener("touchend", onPointerUp, { passive: true });
restartBtn.addEventListener("click", resetGame);

let previous = performance.now();

function animate(now) {
  const delta = Math.min(0.05, (now - previous) / 1000);
  previous = now;

  const radius = 26;
  camera.position.x = Math.sin(orbit.yaw) * radius * Math.cos(orbit.pitch);
  camera.position.y = 5 + Math.sin(orbit.pitch) * 11;
  camera.position.z = Math.cos(orbit.yaw) * radius * Math.cos(orbit.pitch);
  camera.lookAt(0, 2, 0);

  if (!gameOver) {
    remainingTime -= delta;
    updateFish(pirarucus, 30, true);
    updateFish(nativeFish, 31, false);
    syncHud();
    checkGameState();
  }

  renderer.render(scene, camera);
  requestAnimationFrame(animate);
}

resetGame();
resize();
requestAnimationFrame(animate);
