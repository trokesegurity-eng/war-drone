from datetime import datetime, timezone

from app.core.database import DB
from app.schemas.mission import MissionCreate


def create_mission(payload: MissionCreate) -> dict:
    mission = payload.model_dump()
    mission['id'] = len(DB['missions']) + 1
    mission['status'] = 'draft'
    mission['data_inicio'] = None
    mission['data_fim'] = None
    DB['missions'].append(mission)
    return mission


def list_missions() -> list[dict]:
    return DB['missions']


def update_status(mission_id: int, status: str) -> dict:
    for mission in DB['missions']:
        if mission['id'] == mission_id:
            mission['status'] = status
            if status == 'running':
                mission['data_inicio'] = datetime.now(timezone.utc)
            if status == 'finished':
                mission['data_fim'] = datetime.now(timezone.utc)
            return mission
    raise ValueError('Mission not found')
