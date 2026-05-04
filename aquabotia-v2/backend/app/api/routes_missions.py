from fastapi import APIRouter, HTTPException

from app.schemas import MissionCreate, MissionOut
from app.services.mission_service import create_mission, list_missions, update_status

router = APIRouter()


@router.get('/', response_model=list[MissionOut])
def get_missions() -> list[MissionOut]:
    return [MissionOut(**mission) for mission in list_missions()]


@router.post('/', response_model=MissionOut, status_code=201)
def post_mission(payload: MissionCreate) -> MissionOut:
    return MissionOut(**create_mission(payload))


@router.patch('/{mission_id}/start', response_model=MissionOut)
def start_mission(mission_id: int) -> MissionOut:
    try:
        return MissionOut(**update_status(mission_id, 'running'))
    except ValueError as error:
        raise HTTPException(status_code=404, detail=str(error)) from error


@router.patch('/{mission_id}/finish', response_model=MissionOut)
def finish_mission(mission_id: int) -> MissionOut:
    try:
        return MissionOut(**update_status(mission_id, 'finished'))
    except ValueError as error:
        raise HTTPException(status_code=404, detail=str(error)) from error
