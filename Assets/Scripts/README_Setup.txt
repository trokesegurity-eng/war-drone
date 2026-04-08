SETUP MVP (Unity 2D - Realista):
1) Crie a Scene "Level_River_01".
2) Adicione:
   - GameObject "Bootstrap" + GameBootstrap (arraste BiomeConfig, DroneConfig, MissionCatalog).
   - GameObject "GameContext" (opcional, o Bootstrap cria se não existir).
   - GameObject "Input" + InputRouter (opcional: VirtualJoystick).
   - Player Drone prefab:
       - Tag: Player
       - Rigidbody2D (Gravity Scale 0, Interpolate On)
       - Collider2D
       - DroneController2D (referencie InputRouter)
       - SonarSystem (referencie InputRouter; defina LayerMask para targets)
   - Water volume:
       - GameObject "WaterField" + Collider2D (isTrigger=true) + WaterField2D
   - GameObject "MissionSystem" + MissionSystem (referencie SonarSystem e DroneController2D)
   - GameObject "ScoreSystem" + ScoreSystem
   - (Opcional) LeaderboardClientStub + LeaderboardSubmitter
3) Alvos escaneáveis:
   - Crie prefabs com Collider2D e camada "Targets".
   - Use CreatureFactory + CreaturePrefab (CreatureView + CreatureAI2D + Rigidbody2D + Collider2D).
4) Realismo:
   - Use sprites/normal maps + URP 2D Lights (opcional).
   - Turbidez e partículas via ParticleSystem (sedimento e bolhas).

SETUP PROTÓTIPO 3D (Aqua Manta Runner):
1) Crie a Scene "Level_AquaManta_3D".
2) Adicione um GameObject vazio "Prototype3D" com o componente MantaGame3DBuilder.
3) Dê Play.
   - Controles:
     - W/S: acelera/freia
     - A/D: strafe lateral
     - Mouse: direção
     - Q/E: descer/subir
     - Shift ou botão direito do mouse: boost
4) Objetivo:
   - Passe pelos anéis em sequência para pontuar.
   - Administre bateria para manter velocidade alta.
