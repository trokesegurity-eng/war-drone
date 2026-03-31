from fastapi.testclient import TestClient

from app.main import app

client = TestClient(app)


def test_root_status() -> None:
    response = client.get('/')
    assert response.status_code == 200
    assert response.json()['status'] == 'online'


def test_auth_register_and_login() -> None:
    register = client.post(
        '/auth/register',
        json={
            'nome': 'Operador 1',
            'email': 'operador@aquabotia.com',
            'senha': '123456',
            'perfil': 'operador_campo',
        },
    )
    assert register.status_code == 201

    login = client.post('/auth/login', json={'email': 'operador@aquabotia.com', 'senha': '123456'})
    assert login.status_code == 200
    assert login.json()['token_type'] == 'bearer'


def test_create_mission_and_sensor_data() -> None:
    mission = client.post(
        '/missions/',
        json={
            'nome': 'Missão Rio Doce',
            'local_nome': 'Rio Doce',
            'latitude': -19.0,
            'longitude': -40.0,
            'operador_id': 1,
        },
    )
    assert mission.status_code == 201
    mission_id = mission.json()['id']

    sensor = client.post(
        '/sensor-data/',
        json={
            'mission_id': mission_id,
            'temperatura': 26.3,
            'ph': 6.1,
            'turbidez': 12.0,
            'oxigenio_dissolvido': 5.7,
            'profundidade': 4.0,
            'timestamp': '2026-03-31T10:00:00Z',
        },
    )
    assert sensor.status_code == 200

    listing = client.get(f'/sensor-data/mission/{mission_id}')
    assert listing.status_code == 200
    assert len(listing.json()) == 1
