from datetime import datetime

from pydantic import BaseModel, Field


class Mission(BaseModel):
    id: int
    nome: str
    local_nome: str
    latitude: float
    longitude: float
    status: str = "draft"


class SensorDataInput(BaseModel):
    mission_id: int
    temperatura: float
    ph: float = Field(alias="ph")
    turbidez: float
    oxigenio_dissolvido: float
    profundidade: float
    timestamp: datetime


class AlertInput(BaseModel):
    mission_id: int
    tipo: str
    severidade: str
    mensagem: str
