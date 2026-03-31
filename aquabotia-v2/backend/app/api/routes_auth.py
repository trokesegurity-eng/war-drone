from fastapi import APIRouter, HTTPException

from app.schemas.user import UserLogin, UserRegister
from app.services.auth_service import AuthService

router = APIRouter()


@router.post('/register', status_code=201)
def register(payload: UserRegister) -> dict[str, int | str]:
    user = AuthService.register(payload)
    return {"id": user.id, "email": str(user.email), "perfil": user.perfil}


@router.post('/login')
def login(payload: UserLogin) -> dict[str, str]:
    token = AuthService.login(payload)
    if 'error' in token:
        raise HTTPException(status_code=401, detail=token['error'])
    return token
