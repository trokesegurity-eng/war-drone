from datetime import datetime
from typing import Literal

from pydantic import BaseModel


MissionStatus = Literal['draft', 'running', 'finished']


class MissionCreate(BaseModel):
    nome: str
    local_nome: str
    latitude: float
    longitude: float
    operador_id: int


class MissionOut(MissionCreate):
    id: int
    data_inicio: datetime | None = None
    data_fim: datetime | None = None
    status: MissionStatus = 'draft'
