from datetime import datetime

from pydantic import BaseModel


class DetectionInput(BaseModel):
    mission_id: int
    species_id: int | None = None
    species_detected: str
    confianca: float
    frame_url: str
    latitude: float | None = None
    longitude: float | None = None
    timestamp: datetime
