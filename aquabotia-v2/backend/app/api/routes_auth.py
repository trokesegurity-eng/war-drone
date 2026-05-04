from fastapi import APIRouter, HTTPException, status

from app.schemas import LoginInput, TokenOut, UserCreate, UserOut
from app.services.auth_service import login_user, register_user

router = APIRouter()


@router.post('/register', response_model=UserOut, status_code=status.HTTP_201_CREATED)
def register(payload: UserCreate) -> UserOut:
    user = register_user(payload)
    return UserOut(**user)


@router.post('/login', response_model=TokenOut)
def login(payload: LoginInput) -> TokenOut:
    token = login_user(payload.email, payload.senha)
    if token is None:
        raise HTTPException(status_code=status.HTTP_401_UNAUTHORIZED, detail='Credenciais inválidas')
    return TokenOut(access_token=token)
