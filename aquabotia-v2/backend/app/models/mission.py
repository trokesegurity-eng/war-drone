from pydantic import BaseModel


class Mission(BaseModel):
    id: int
    nome: str
    local_nome: str
    latitude: float
    longitude: float
    status: str = "draft"
    operador_id: int | None = None
