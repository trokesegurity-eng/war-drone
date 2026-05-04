# Arquitetura Técnica V2

## Fluxo macro

`Drone/Sensores -> IA Embarcada -> Gateway/API -> Banco de Dados -> Dashboard/App`

## Fluxo detalhado

1. O drone coleta vídeo e telemetria de sensores.
2. A IA embarcada roda inferência local inicial.
3. Os dados são enviados ao gateway/API.
4. A API valida, processa e armazena.
5. O dashboard e o app exibem tempo real + histórico.
6. Regras de negócio disparam alertas de risco.

## Serviços-alvo

- Auth
- Missions
- Sensors
- Vision
- Alerts
- Catalog
