from app.core.database import db
from app.models.alert import Alert
from app.schemas.alert import AlertCreate


class AlertService:
    @staticmethod
    def create(payload: AlertCreate) -> Alert:
        alert = Alert(id=len(db.alerts) + 1, **payload.model_dump())
        db.alerts.append(alert)
        return alert

    @staticmethod
    def list_all() -> list[Alert]:
        return db.alerts
