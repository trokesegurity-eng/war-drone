# AquaManta: Neon Rift

Jogo arcade premium inspirado na identidade visual da manta cibernética da imagem de referência.

## Visão do jogo
- **Gênero:** action runner subaquático com risco/recompensa.
- **Loop principal:** coletar nós energéticos, evitar predadores digitais e administrar energia da manta-drone.
- **Diferenciais de alto nível:**
  - Sonar Pulse com física de repulsão tática.
  - Dificuldade progressiva por setor.
  - Renderização neon estilizada em canvas, otimizada para 60 FPS.
  - Suporte unificado para web, mobile (touch + PWA) e desktop (browser/fullscreen).

## Controles
- **PC:** WASD/Setas para mover, `Space` para sonar pulse, `Shift` para boost.
- **Mobile/Web touch:** arraste no canvas para direção, botões `Pulse` e `Boost`.

## Rodar localmente
```bash
npm run lint
npm test
npm run start
```
Depois abra `http://localhost:4173`.

## Plataformas
### Web
Deploy estático (Vercel/Netlify/GitHub Pages).

### Mobile
- Funciona como **PWA** instalável em Android/iOS (Add to Home Screen).
- Touch controls já incluídos.

### PC
- Funciona no browser desktop.
- Pode ser empacotado com Electron/Tauri sem mudanças de gameplay.

## Arquitetura
- `src/main.js`: loop do jogo, render, input e estados.
- `src/core.js`: funções puras (colisão, movimento e dificuldade), cobertas por testes.
- `tests/core.test.js`: validações de gameplay crítico.

## Roadmap profissional sugerido
1. Sistema de missões dinâmicas com seed determinística.
2. Ranked global + replay ghost.
3. IA de inimigos com behavior trees.
4. Shader pipeline WebGL para água volumétrica.
