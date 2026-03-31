from fastapi import APIRouter

from app.services.upload_service import UploadService

router = APIRouter()


@router.post('/upload')
def upload_detection(filename: str) -> dict[str, str]:
    return UploadService.register_detection(filename)
