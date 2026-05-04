from fastapi import APIRouter

from app.schemas import SpeciesCreate, SpeciesOut
from app.services.species_service import create_species, list_species

router = APIRouter()


@router.get('/', response_model=list[SpeciesOut])
def get_species() -> list[SpeciesOut]:
    return [SpeciesOut(**item) for item in list_species()]


@router.post('/', response_model=SpeciesOut, status_code=201)
def post_species(payload: SpeciesCreate) -> SpeciesOut:
    return SpeciesOut(**create_species(payload))
