from fastapi import APIRouter, HTTPException

from app.schemas import AlertInput, AlertOut
from app.services.alert_service import create_alert, list_alerts, resolve_alert

router = APIRouter()


@router.get('/', response_model=list[AlertOut])
def get_alerts() -> list[AlertOut]:
    return [AlertOut(**item) for item in list_alerts()]


@router.post('/', response_model=AlertOut, status_code=201)
def post_alert(payload: AlertInput) -> AlertOut:
    return AlertOut(**create_alert(payload))


@router.patch('/{alert_id}/resolve', response_model=AlertOut)
def patch_resolve(alert_id: int) -> AlertOut:
    try:
        return AlertOut(**resolve_alert(alert_id))
    except ValueError as error:
        raise HTTPException(status_code=404, detail=str(error)) from error
