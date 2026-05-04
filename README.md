# war-drone

Este repositório agora inclui um protótipo complementar do **AgroSwarm AI**, focado em apresentação web premium e backend MVP para analytics agrícolas com drones.

## Conteúdo adicionado
- `docs/agroswarm-ai-dossier.md`: documento para arquivar com pitch, arquitetura SaaS, plano resumido e roadmap.
- `web/index.html`: apresentação web premium com dashboard, mapa, animações e integração com API.
- `backend/main.py`: API FastAPI para status, NDVI demo, frota e detecção visual de anomalias/pragas.
- `backend/requirements.txt`: dependências do backend.

## Como rodar o backend
```bash
cd backend
python -m venv .venv
source .venv/bin/activate
pip install -r requirements.txt
uvicorn main:app --reload
```

A API ficará disponível em `http://127.0.0.1:8000` e a documentação interativa em `http://127.0.0.1:8000/docs`.

## Como abrir a apresentação web
Basta abrir `web/index.html` no navegador. Por padrão, a página tenta acessar a API em `http://127.0.0.1:8000`.

Se quiser apontar para outro backend, execute no console do navegador:
```js
localStorage.setItem('agroswarm-api-base', 'https://seu-backend.exemplo.com')
```

## Observação sobre a IA agrícola
O endpoint de detecção de pragas/anomalias deste MVP usa heurísticas de visão computacional com OpenCV para localizar regiões suspeitas. Para produção, o ideal é evoluir para um modelo treinado por cultura e tipo de praga/doença.
