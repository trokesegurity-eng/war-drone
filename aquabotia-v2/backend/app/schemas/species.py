from typing import Literal

from pydantic import BaseModel


class SpeciesCreate(BaseModel):
    nome_comum: str
    nome_cientifico: str
    categoria: str
    nativa_ou_invasora: Literal['nativa', 'invasora', 'monitorar_contexto_regional']
    bioma: str
    observacoes: str | None = None


class SpeciesOut(SpeciesCreate):
    id: int
