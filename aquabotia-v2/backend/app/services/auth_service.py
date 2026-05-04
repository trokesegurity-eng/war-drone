from datetime import datetime, timezone

from app.core.database import DB
from app.core.security import create_access_token, hash_password, verify_password
from app.schemas.user import UserCreate


def register_user(payload: UserCreate) -> dict:
    user_id = len(DB['users']) + 1
    user = {
        'id': user_id,
        'nome': payload.nome,
        'email': payload.email,
        'senha_hash': hash_password(payload.senha),
        'perfil': payload.perfil,
        'criado_em': datetime.now(timezone.utc),
    }
    DB['users'].append(user)
    return user


def login_user(email: str, senha: str) -> str | None:
    for user in DB['users']:
        if user['email'] == email and verify_password(senha, user['senha_hash']):
            return create_access_token(subject=email)
    return None
