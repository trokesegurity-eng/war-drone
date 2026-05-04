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
- `GET /sensor-data/missions/{mission_id}`
- `GET /sensor-data/health`

## Espécies
- `GET /species/`
- `POST /species/`

## Detecções
- `POST /detections/upload`
- `GET /missions/{mission_id}/detections`

## Alertas
- `GET /alerts/`
- `POST /alerts/`
- `PATCH /alerts/{id}/resolve`
