from datetime import datetime

from pydantic import BaseModel


class SensorDataInput(BaseModel):
    mission_id: int
    temperatura: float
    ph: float
    turbidez: float
    oxigenio_dissolvido: float
    profundidade: float
    timestamp: datetime
