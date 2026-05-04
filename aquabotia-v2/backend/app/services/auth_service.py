from app.core.database import db
from app.core.security import fake_jwt_for_user, hash_password
from app.models.user import User
from app.schemas.user import UserLogin, UserRegister


class AuthService:
    @staticmethod
    def register(payload: UserRegister) -> User:
        user = User(
            id=len(db.users) + 1,
            nome=payload.nome,
            email=payload.email,
            senha_hash=hash_password(payload.senha),
            perfil=payload.perfil,
        )
        db.users.append(user)
        return user

    @staticmethod
    def login(payload: UserLogin) -> dict[str, str]:
        hashed = hash_password(payload.senha)
        user = next(
            (u for u in db.users if u.email == payload.email and u.senha_hash == hashed),
            None,
        )
        if user is None:
            return {"error": "credenciais inválidas"}
        return {"access_token": fake_jwt_for_user(str(payload.email)), "token_type": "bearer"}
