from datetime import datetime
from typing import Literal

from pydantic import BaseModel, EmailStr


UserRole = Literal['administrador', 'tecnico_ambiental', 'pesquisador', 'orgao_publico', 'operador_campo']


class UserCreate(BaseModel):
    nome: str
    email: EmailStr
    senha: str
    perfil: UserRole


class UserOut(BaseModel):
    id: int
    nome: str
    email: EmailStr
    perfil: UserRole
    criado_em: datetime
