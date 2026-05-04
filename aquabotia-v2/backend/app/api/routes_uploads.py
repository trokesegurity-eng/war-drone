from datetime import datetime, timezone

from fastapi import APIRouter

from app.core.database import DB
from app.schemas import DetectionInput
from app.services.upload_service import build_detection_url

router = APIRouter()


@router.post('/detections/upload')
def upload_detection(payload: DetectionInput) -> dict:
    detection = payload.model_dump()
    detection['id'] = len(DB['detections']) + 1
    detection['imagem_url'] = build_detection_url(f"frame_{detection['id']:03d}.jpg")
    detection['registrado_em'] = datetime.now(timezone.utc)
    DB['detections'].append(detection)
    return detection


@router.get('/missions/{mission_id}/detections')
def get_mission_detections(mission_id: int) -> list[dict]:
    return [item for item in DB['detections'] if item['mission_id'] == mission_id]
