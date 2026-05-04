from datetime import UTC, datetime

from pydantic import BaseModel, Field


class Alert(BaseModel):
    id: int
    mission_id: int
    tipo: str
    severidade: str
    mensagem: str
    status: str = "open"
    criado_em: datetime = Field(default_factory=lambda: datetime.now(UTC))
