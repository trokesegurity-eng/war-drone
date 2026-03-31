from pydantic import BaseModel


class SpeciesCreate(BaseModel):
    nome_comum: str
    nome_cientifico: str
    categoria: str
    nativa_ou_invasora: str
    bioma: str | None = None
