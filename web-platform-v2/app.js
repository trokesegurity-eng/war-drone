const state = {
  version: [2, 1, 0],
  layers: [
    'Drone subaquático inteligente',
    'IA embarcada (edge inference)',
    'Gateway/API central',
    'Plataforma Web/Mobile',
    'Núcleo de dados ambientais e catalogação biológica'
  ],
  cells: [
    ['Célula de Produto', ['visão do produto', 'backlog', 'desenho do MVP', 'validação com mercado']],
    ['IA & Visão Computacional', ['treinamento de modelos', 'classificação de espécies', 'detecção de anomalias']],
    ['Sistemas Embarcados', ['integração de sensores', 'comunicação com estação', 'automação da missão']],
    ['Plataforma Web/Mobile', ['dashboard', 'mapas', 'app de campo', 'gerenciamento de missões']],
    ['Dados Ambientais', ['padronização de dados', 'relatórios', 'analytics', 'histórico por região']]
  ],
  mvp: [
    ['Módulo 1 — Coleta ambiental', ['temperatura', 'pH', 'turbidez', 'oxigênio dissolvido', 'profundidade', 'GPS']],
    ['Módulo 2 — Visão computacional', ['captura subaquática', 'identificação básica', 'detecção de invasoras']],
    ['Módulo 3 — Plataforma', ['painel web', 'mapa de pontos', 'histórico', 'alertas']],
    ['Módulo 4 — App de campo', ['iniciar missão', 'registro manual', 'evidência fotográfica', 'consulta rápida']]
  ],
  stack: {
    Backend: 'FastAPI + JWT + filas + REST',
    IA: 'PyTorch/TensorFlow + OpenCV + YOLO + ONNX/TensorRT',
    Embarcado: 'Python/C++ + Jetson Nano/Orin Nano/Raspberry Pi',
    Web: 'React + Next.js + Tailwind + Leaflet/Mapbox',
    Mobile: 'Flutter ou React Native',
    Dados: 'PostgreSQL/PostGIS + Redis + MinIO/S3',
    Infra: 'Docker + Nginx + CI/CD'
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
    'API valida, processa e persiste no banco',
    'Dashboard/App exibem tempo real e histórico',
    'Regras de negócio geram alertas automáticos'
  ],
  folders: [
    'aquabotia-v2/',
    '├── docs/',
    '├── backend/app/{core,api,models,schemas,services,workers}',
    '├── ai/{datasets,training,inference,models}',
    '├── embedded/',
    '├── web/src/',
    '├── mobile/lib/',
    '├── infra/{docker-compose.yml,nginx.conf,k8s/}',
    '└── scripts/'
  ],
  schema: [
    'users(id, nome, email, senha_hash, perfil, criado_em)',
    'missions(id, nome, local_nome, latitude, longitude, data_inicio, data_fim, status, operador_id)',
    'sensor_data(id, mission_id, timestamp, temperatura, ph, turbidez, oxigenio_dissolvido, profundidade)',
    'species(id, nome_comum, nome_cientifico, categoria, nativa_ou_invasora, bioma, observacoes)',
    'detections(id, mission_id, species_id, imagem_url, confianca, timestamp, latitude, longitude)',
    'alerts(id, mission_id, tipo, severidade, mensagem, status, criado_em)'
  ]
};

const card = (title, content) => `<article class="card"><h3>${title}</h3>${content}</article>`;
const list = (items) => `<ul>${items.map((item) => `<li>${item}</li>`).join('')}</ul>`;

document.querySelector('#layers').innerHTML = state.layers.map((layer, i) => card(`Camada ${i + 1}`, `<p>${layer}</p>`)).join('');
document.querySelector('#cells').innerHTML = state.cells.map(([name, feats]) => card(name, list(feats))).join('');
document.querySelector('#mvp').innerHTML = state.mvp.map(([name, feats]) => card(name, list(feats))).join('');
document.querySelector('#stack').innerHTML = Object.entries(state.stack).map(([name, desc]) => card(name, `<p>${desc}</p>`)).join('');
document.querySelector('#services').innerHTML = state.services.map((svc) => card(svc, '<p>Pronto para implementação incremental.</p>')).join('');
document.querySelector('#flow').innerHTML = `<ol>${state.flow.map((step) => `<li>${step}</li>`).join('')}</ol>`;
document.querySelector('#folders').textContent = state.folders.join('\n');
document.querySelector('#schema').textContent = state.schema.join('\n');

document.querySelector('#bump').addEventListener('click', () => {
  state.version[2] += 1;
  document.querySelector('#version').textContent = `V${state.version.join('.')}`;
});
