from app.core.database import db
from app.models.sensor_data import SensorData
from app.schemas.sensor_data import SensorDataInput


class SensorService:
    @staticmethod
    def create(payload: SensorDataInput) -> SensorData:
        data = SensorData(id=len(db.sensor_data) + 1, **payload.model_dump())
        db.sensor_data.append(data)
        return data

    @staticmethod
    def list_by_mission(mission_id: int) -> list[SensorData]:
        return [item for item in db.sensor_data if item.mission_id == mission_id]
