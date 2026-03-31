# API Spec Inicial (V2)

## Auth
- `POST /auth/register`
- `POST /auth/login`

## Missões
- `GET /missions/`
- `POST /missions/`
- `PATCH /missions/{id}/start`
- `PATCH /missions/{id}/finish`

## Sensores
- `POST /sensor-data/`
- `GET /sensor-data/mission/{mission_id}`
- `GET /sensor-data/health`

## Espécies
- `GET /species/`

## Alertas
- `GET /alerts/`
- `POST /alerts/`

## Detecções
- `POST /detections/upload?filename=<nome_arquivo>`
