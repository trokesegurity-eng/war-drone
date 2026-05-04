from app.core.database import DB
from app.schemas.alert import AlertInput
from app.schemas.sensor_data import SensorDataInput
from app.services.alert_service import create_alert
from app.services.rules import evaluate_sensor_thresholds


def create_sensor_data(payload: SensorDataInput) -> dict:
    measurement = payload.model_dump()
    measurement['id'] = len(DB['sensor_data']) + 1
    DB['sensor_data'].append(measurement)

    for event in evaluate_sensor_thresholds(measurement):
        create_alert(AlertInput(mission_id=measurement['mission_id'], **event))

    return measurement


def list_mission_sensor_data(mission_id: int) -> list[dict]:
    return [row for row in DB['sensor_data'] if row['mission_id'] == mission_id]
