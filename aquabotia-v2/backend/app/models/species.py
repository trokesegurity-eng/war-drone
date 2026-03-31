from pydantic import BaseModel


class Species(BaseModel):
    id: int
    nome_comum: str
    nome_cientifico: str
    categoria: str
    nativa_ou_invasora: str
    bioma: str | None = None
