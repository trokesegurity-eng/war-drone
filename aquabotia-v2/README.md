# Aquabotia Software Factory — V2

V2 técnica com base funcional para backend, fluxo embarcado mockado e arquitetura de produto.

## Estrutura do monorepo (base)

- `docs/`: visão geral, arquitetura, roadmap, sensores e API.
- `backend/`: API FastAPI com módulos de auth, missões, sensores, espécies, alertas e uploads.
- `ai/`: espaço para treinamento, inferência e modelos.
- `embedded/`: coleta mockada de sensores e cliente de comunicação.
- `web/`: estrutura inicial para dashboard (React/Next).
- `mobile/`: estrutura inicial para app de campo.
- `infra/`: `docker-compose`, `nginx` e base para `k8s`.
- `scripts/`: automações (seed e init de banco).

## Como executar backend

```bash
cd aquabotia-v2/backend
python -m venv .venv
source .venv/bin/activate
pip install -r requirements.txt
uvicorn app.main:app --reload
```

## Qualidade

```bash
ruff check app tests
pytest
```
