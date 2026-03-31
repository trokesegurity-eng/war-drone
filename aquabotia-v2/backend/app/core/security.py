import hashlib
from datetime import UTC, datetime, timedelta


def hash_password(raw_password: str) -> str:
    return hashlib.sha256(raw_password.encode("utf-8")).hexdigest()


def fake_jwt_for_user(email: str) -> str:
    expires = datetime.now(UTC) + timedelta(hours=12)
    return f"token::{email}::{int(expires.timestamp())}"
