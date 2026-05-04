from datetime import datetime, timedelta, timezone
import hashlib
import hmac

from app.core.config import settings


def hash_password(password: str) -> str:
    return hashlib.sha256(password.encode('utf-8')).hexdigest()


def verify_password(password: str, password_hash: str) -> bool:
    return hmac.compare_digest(hash_password(password), password_hash)


def create_access_token(subject: str, expires_minutes: int = 60) -> str:
    expires_at = (datetime.now(timezone.utc) + timedelta(minutes=expires_minutes)).isoformat()
    payload = f'{subject}|{expires_at}|{settings.jwt_secret}'
    return hashlib.sha256(payload.encode('utf-8')).hexdigest()
