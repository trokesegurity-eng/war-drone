from fastapi import APIRouter

from app.schemas.mission import MissionCreate
from app.services.mission_service import MissionService

router = APIRouter()


@router.get('/')
def list_missions():
    return MissionService.list_all()


@router.post('/', status_code=201)
def create_mission(mission: MissionCreate):
    return MissionService.create(mission)


@router.patch('/{mission_id}/start')
def start_mission(mission_id: int) -> dict[str, str | int]:
    return {"mission_id": mission_id, "status": "running"}


@router.patch('/{mission_id}/finish')
def finish_mission(mission_id: int) -> dict[str, str | int]:
    return {"mission_id": mission_id, "status": "finished"}
