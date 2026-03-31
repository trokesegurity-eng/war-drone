from fastapi import APIRouter

from app.schemas.sensor_data import SensorDataInput
from app.services.sensor_service import SensorService

router = APIRouter()


@router.post('/')
def create_sensor_data(data: SensorDataInput):
    payload = SensorService.create(data)
    return {"message": "Dado de sensor recebido com sucesso", "payload": payload}


@router.get('/mission/{mission_id}')
def list_mission_sensor_data(mission_id: int):
    return SensorService.list_by_mission(mission_id)


@router.get('/health')
def health() -> dict[str, str]:
    return {"sensor_service": "ok"}
