const state = {
  version: [1, 0, 0],
  nucleos: [
    {
      nome: '🧩 Núcleo 1 — IA & Visão Computacional',
      responsavel: [
        'Reconhecimento de peixes (nativos vs invasores)',
        'Detecção de poluição',
        'Análise de comportamento animal',
        'Treinamento de modelos'
      ],
      tecnologias: 'Python, TensorFlow/PyTorch, OpenCV, YOLO'
    },
    {
      nome: '🤖 Núcleo 2 — Sistemas Embarcados (Drone)',
      responsavel: [
        'IA rodando no drone (edge computing)',
        'Integração de sensores (pH, turbidez, temperatura, oxigênio)',
        'Navegação autônoma'
      ],
      tecnologias: 'ROS, NVIDIA Jetson/Raspberry Pi, C++, Python'
    },
    {
      nome: '🌐 Núcleo 3 — Plataforma Digital (Web + Mobile)',
      responsavel: ['Dashboard ambiental', 'Monitoramento em tempo real', 'Mapas de rios e lagos', 'Alertas automáticos'],
      tecnologias: 'React/Flutter, Node.js/Python, AWS/Google Cloud'
    },
    {
      nome: '📊 Núcleo 4 — Dados & Inteligência Ambiental',
      responsavel: ['Banco de dados ecológico', 'Modelos preditivos', 'Relatórios ambientais'],
      tecnologias: 'Data Lake + BI + ML'
    },
    {
      nome: '🎮 Núcleo 5 — Simulação & Digital Twin',
      responsavel: ['Simular rios, lagos e represas', 'Treinar IA antes do mundo real', 'Criar ambiente estilo game'],
      tecnologias: 'Unity ou Unreal Engine'
    }
  ],
  mvps: [
    {
      nome: 'MVP 1 — Monitoramento Inteligente',
      itens: [
        'Identificação de espécies',
        'Detecção de peixe invasor (ex: pirarucu fora da Amazônia)',
        'Qualidade da água em tempo real',
        'Dashboard com mapa'
      ]
    },
    {
      nome: 'MVP 2 — IA de Catalogação',
      itens: ['Classificação automática de espécies', 'Base de dados brasileira', 'Treinamento contínuo']
    },
    {
      nome: 'MVP 3 — App Ambiental',
      itens: ['Visualizar dados', 'Receber alertas', 'Registrar ocorrências'],
      publico: 'Governo, ONGs, Pesquisadores, Pescadores'
    }
  ],
  roadmap: [
    {
      fase: '🟢 Fase 1 (0–3 meses)',
      metas: ['Protótipo do drone', 'IA básica de detecção', 'App simples']
    },
    {
      fase: '🟡 Fase 2 (3–6 meses)',
      metas: ['Testes em rios reais', 'Dashboard completo', 'Base de dados inicial']
    },
    {
      fase: '🔵 Fase 3 (6–12 meses)',
      metas: ['Escala nacional', 'Parcerias com governo', 'IA avançada']
    }
  ],
  time: [
    'Engenheiro de IA (2)',
    'Engenheiro embarcado (2)',
    'Dev backend (1–2)',
    'Dev frontend/mobile (1–2)',
    'Especialista ambiental (1)',
    'Designer UX (1)'
  ]
};

function renderCard(title, items, subtitle = '') {
  const itemsHtml = items.map((item) => `<li>${item}</li>`).join('');
  return `
    <article class="card">
      <h3>${title}</h3>
      <ul>${itemsHtml}</ul>
      ${subtitle ? `<p class="tech"><strong>Tecnologias:</strong> ${subtitle}</p>` : ''}
    </article>
  `;
}

function render() {
  const nucleosGrid = document.querySelector('#nucleos-grid');
  const mvpsGrid = document.querySelector('#mvps-grid');
  const roadmapTimeline = document.querySelector('#roadmap-timeline');
  const teamList = document.querySelector('#team-list');

  nucleosGrid.innerHTML = state.nucleos.map((n) => renderCard(n.nome, n.responsavel, n.tecnologias)).join('');

  mvpsGrid.innerHTML = state.mvps
    .map((mvp) => renderCard(mvp.nome, mvp.itens, mvp.publico ? `Público: ${mvp.publico}` : ''))
    .join('');

  roadmapTimeline.innerHTML = state.roadmap.map((fase) => renderCard(fase.fase, fase.metas)).join('');

  teamList.innerHTML = state.time.map((item) => `<li>${item}</li>`).join('');
}

function bumpVersion() {
  state.version[2] += 1;
  const versionString = `V${state.version.join('.')}`;
  document.querySelector('#platform-version').textContent = versionString;
}

document.querySelector('#next-version-btn').addEventListener('click', bumpVersion);

render();
