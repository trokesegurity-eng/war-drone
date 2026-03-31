from fastapi import APIRouter

from app.services.species_service import SpeciesService

router = APIRouter()


@router.get('/')
def list_species():
    return SpeciesService.list_all()
