from pydantic import BaseModel


class MissionCreate(BaseModel):
    nome: str
    local_nome: str
    latitude: float
    longitude: float
    operador_id: int | None = None
