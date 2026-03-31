from app.core.database import db
from app.models.mission import Mission
from app.schemas.mission import MissionCreate


class MissionService:
    @staticmethod
    def create(payload: MissionCreate) -> Mission:
        mission = Mission(id=len(db.missions) + 1, **payload.model_dump(), status="created")
        db.missions.append(mission)
        return mission

    @staticmethod
    def list_all() -> list[Mission]:
        return db.missions
