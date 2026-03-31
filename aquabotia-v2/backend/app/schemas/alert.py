from pydantic import BaseModel


class AlertCreate(BaseModel):
    mission_id: int
    tipo: str
    severidade: str
    mensagem: str
