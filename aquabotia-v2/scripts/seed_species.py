import requests

API_URL = 'http://localhost:8000/species/'

SEEDS = [
    {
        'nome_comum': 'Tucunaré',
        'nome_cientifico': 'Cichla ocellaris',
        'categoria': 'peixe',
        'nativa_ou_invasora': 'nativa',
        'bioma': 'Amazônia',
        'observacoes': 'Seed inicial'
    },
    {
        'nome_comum': 'Pirarucu',
        'nome_cientifico': 'Arapaima gigas',
        'categoria': 'peixe',
        'nativa_ou_invasora': 'monitorar_contexto_regional',
        'bioma': 'Amazônia'
    }
]

for item in SEEDS:
    print(requests.post(API_URL, json=item, timeout=10).status_code)
