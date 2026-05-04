from datetime import datetime
from typing import Literal

from pydantic import BaseModel


class AlertInput(BaseModel):
    mission_id: int
    tipo: str
    severidade: Literal['baixa', 'media', 'alta']
    mensagem: str


class AlertOut(AlertInput):
    id: int
    status: Literal['open', 'resolved'] = 'open'
    criado_em: datetime
