from app.core.database import DB
from app.schemas.species import SpeciesCreate


def list_species() -> list[dict]:
    return DB['species']


def create_species(payload: SpeciesCreate) -> dict:
    species = payload.model_dump()
    species['id'] = len(DB['species']) + 1
    DB['species'].append(species)
    return species
