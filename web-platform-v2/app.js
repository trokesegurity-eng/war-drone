const state = {
  version: [2, 0, 0],
  layers: [
    'Drone subaquático inteligente',
    'IA embarcada (edge inference)',
    'Gateway/API central',
    'Plataforma Web/Mobile',
    'Núcleo de dados ambientais'
  ],
  mvp: [
    ['Coleta ambiental', ['temperatura', 'pH', 'turbidez', 'oxigênio dissolvido', 'profundidade']],
    ['Visão computacional', ['captura subaquática', 'identificação básica', 'detecção de invasoras']],
    ['Plataforma', ['painel web', 'mapa de pontos', 'histórico', 'alertas']],
    ['App de campo', ['iniciar missão', 'registro manual', 'evidência fotográfica', 'consulta rápida']]
  ],
  stack: {
    Backend: 'FastAPI + JWT + filas',
    IA: 'PyTorch/TensorFlow + OpenCV + YOLO + ONNX/TensorRT',
    Embarcado: 'Python/C++ + Jetson/Raspberry Pi',
    Web: 'React + Next.js + Tailwind + Leaflet/Mapbox',
    Mobile: 'Flutter ou React Native',
    Dados: 'PostgreSQL/PostGIS + Redis + MinIO/S3'
  },
  services: [
    'auth-service',
    'mission-service',
    'sensor-service',
    'vision-service',
    'alerts-service',
    'catalog-service'
  ],
  flow: [
    'Drone coleta vídeo e sensores',
    'IA embarcada roda inferência inicial',
    'Gateway envia eventos para API',
    'API valida e grava no banco',
    'Dashboard/App exibem tempo real e histórico',
    'Regra de negócio gera alertas'
  ],
  schema: [
    'users(id, nome, email, senha_hash, perfil, criado_em)',
    'missions(id, nome, latitude, longitude, data_inicio, data_fim, status, operador_id)',
    'sensor_data(id, mission_id, timestamp, temperatura, ph, turbidez, oxigenio_dissolvido, profundidade)',
    'species(id, nome_comum, nome_cientifico, categoria, nativa_ou_invasora, bioma)',
    'detections(id, mission_id, species_id, imagem_url, confianca, timestamp, latitude, longitude)',
    'alerts(id, mission_id, tipo, severidade, mensagem, status, criado_em)'
  ]
};

const card = (title, content) => `
  <article class="card">
    <h3>${title}</h3>
    ${content}
  </article>
`;

document.querySelector('#layers').innerHTML = state.layers
  .map((layer, i) => card(`Camada ${i + 1}`, `<p>${layer}</p>`))
  .join('');

document.querySelector('#mvp').innerHTML = state.mvp
  .map(([name, feats]) => card(name, `<ul>${feats.map((f) => `<li>${f}</li>`).join('')}</ul>`))
  .join('');

document.querySelector('#stack').innerHTML = Object.entries(state.stack)
  .map(([name, desc]) => card(name, `<p>${desc}</p>`))
  .join('');

document.querySelector('#services').innerHTML = state.services
  .map((svc) => card(svc, '<p>Pronto para implementação incremental.</p>'))
  .join('');

document.querySelector('#flow').innerHTML = `<ol>${state.flow.map((step) => `<li>${step}</li>`).join('')}</ol>`;
document.querySelector('#schema').textContent = state.schema.join('\n');

document.querySelector('#bump').addEventListener('click', () => {
  state.version[2] += 1;
  document.querySelector('#version').textContent = `V${state.version.join('.')}`;
});
