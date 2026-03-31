from fastapi import APIRouter

from app.schemas.alert import AlertCreate
from app.services.alert_service import AlertService

router = APIRouter()


@router.get('/')
def list_alerts():
    return AlertService.list_all()


@router.post('/', status_code=201)
def create_alert(alert: AlertCreate):
    return AlertService.create(alert)
