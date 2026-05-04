from pydantic import BaseModel, EmailStr


class UserRegister(BaseModel):
    nome: str
    email: EmailStr
    senha: str
    perfil: str = "operador_campo"


class UserLogin(BaseModel):
    email: EmailStr
    senha: str
