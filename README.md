# WAR AquaDrone

Base inicial de arquitetura Unity C# para o jogo **WAR AquaDrone** (mobile + web), inspirada no conceito de missões subaquáticas furtivas/miméticas.

## O que foi implementado

- Arquitetura modular com `GameManager`, `StateMachine` e carregamento de cenas.
- Núcleo de gameplay do drone:
  - movimento subaquático arcade,
  - energia,
  - assinatura sonora (stealth),
  - sonar.
- Sistema de modos:
  - `Normal`,
  - `Stealth`,
  - `Mimetic`.
- Sistema de loadout por peças (`DronePart`) com multiplicadores de stats.
- Missões baseadas em objetivos plugáveis (`ObjectiveBase`).
- Progressão MVP com perfil, economia, inventário e upgrades.
- UI base para menu, hangar, HUD e tela de recompensas.

## Estrutura

A estrutura principal está em `Assets/_Project/Scripts` organizada por domínios:

- `App/`
- `Core/`
- `Input/`
- `Gameplay/`
- `UI/`

## Próximos passos sugeridos

1. Criar cenas Unity (`Boot`, `Menu`, `Hangar`, `Mission_River_01`).
2. Configurar tags/layers (`Player`, máscaras de sonar).
3. Criar `ScriptableObjects`:
   - `AppConfig`,
   - `DroneConfig`,
   - `MissionDefinition`,
   - `DronePart`.
4. Implementar vertical slice de missão (Rio ou Represa) com 3 objetivos encadeados.
5. Integrar Input System (mobile/web) e persistência mais robusta.

## Histórico

A árvore inicial recebida antes da implementação foi preservada em:

- `docs/initial_assets_tree.txt`
