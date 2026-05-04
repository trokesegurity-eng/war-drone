from fastapi import APIRouter

from app.schemas import SensorDataInput
from app.services.sensor_service import create_sensor_data, list_mission_sensor_data

router = APIRouter()


@router.post('/')
def post_sensor_data(data: SensorDataInput) -> dict:
    return {
        'message': 'Dado de sensor recebido com sucesso',
        'payload': create_sensor_data(data),
    }


@router.get('/missions/{mission_id}')
def get_mission_sensor_data(mission_id: int) -> list[dict]:
    return list_mission_sensor_data(mission_id)


@router.get('/health')
def health() -> dict[str, str]:
    return {'sensor_service': 'ok'}
