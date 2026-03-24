# war-drone
simulador drone subaquatico

## Dados MVP Brasil (pt-BR)
- Arquivo de configuração inicial: `data/mvp-br-0.1.pt-BR.json`
- Inclui biome pack da Mata Atlântica, catálogo de espécies/ambiente, missões e regras de pontuação.

## Novo protótipo: jogo educativo 3D (web, mobile e PC)
Foi adicionado um protótipo jogável em `webgame/` com o tema **"Caça ao Pirarucu"**.

### Objetivo pedagógico
- Sensibilizar sobre impactos da introdução de espécies fora do ecossistema de origem.
- Simular a remoção de pirarucus invasores para proteger fauna local fora da Amazônia.

### Como executar
1. Abra o arquivo `webgame/index.html` no navegador (Chrome, Edge, Firefox ou Safari).
2. Em ambiente local com servidor HTTP (recomendado), rode por exemplo:
   - `python3 -m http.server 8080`
   - depois acesse `http://localhost:8080/webgame/`

### Controles
- **PC:** arrastar para mover câmera + clique para capturar pirarucus vermelhos.
- **Mobile:** deslizar para mover câmera + toque para capturar.

### Mecânicas implementadas
- Cena 3D submarina com peixes invasores (pirarucu) e peixes nativos.
- HUD com pontuação, tempo e saúde do ecossistema.
- Condições de vitória/derrota e botão de reinício.
