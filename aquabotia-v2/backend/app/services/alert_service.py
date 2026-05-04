from datetime import datetime, timezone

from app.core.database import DB
from app.schemas.alert import AlertInput


def create_alert(payload: AlertInput) -> dict:
    alert = payload.model_dump()
    alert['id'] = len(DB['alerts']) + 1
    alert['status'] = 'open'
    alert['criado_em'] = datetime.now(timezone.utc)
    DB['alerts'].append(alert)
    return alert


def list_alerts() -> list[dict]:
    return DB['alerts']


def resolve_alert(alert_id: int) -> dict:
    for alert in DB['alerts']:
        if alert['id'] == alert_id:
            alert['status'] = 'resolved'
            return alert
    raise ValueError('Alert not found')
