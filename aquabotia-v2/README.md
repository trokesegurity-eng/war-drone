# Aquabotia Software Factory — V2

Arquitetura técnica + base funcional do MVP Aquabotia 1.0.

## Camadas do sistema

1. Drone subaquático inteligente
2. IA embarcada
3. Gateway/API central
4. Plataforma web/mobile
5. Núcleo de dados ambientais e catalogação

## Estrutura sugerida (monorepo)

- `docs/` documentação técnica
- `backend/` FastAPI + regras de negócio iniciais
- `embedded/` simulação local de sensores e comunicação
- `web/` base de frontend (Next.js)
- `mobile/` base de aplicativo de campo (Flutter)
- `infra/` docker-compose e nginx
- `scripts/` automações de bootstrap

## MVP funcional implementado nesta base

- Cadastro e gestão de missões (`/missions`)
- Ingestão de sensores (`/sensor-data`)
- Regras automáticas de alertas por limiar (pH, oxigênio, turbidez)
- Catálogo biológico (`/species`)
- Upload lógico de detecções (`/detections/upload`)
- Auth inicial (`/auth/register`, `/auth/login`)

## Rodando localmente

```bash
cd aquabotia-v2/infra
docker compose up --build
```

API disponível em `http://localhost:8000`.
