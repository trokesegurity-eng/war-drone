from datetime import datetime

from pydantic import BaseModel


class SensorData(BaseModel):
    id: int
    mission_id: int
    timestamp: datetime
    temperatura: float
    ph: float
    turbidez: float
    oxigenio_dissolvido: float
    profundidade: float
