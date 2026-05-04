from datetime import datetime, timezone


def capture_frame() -> str:
    stamp = datetime.now(timezone.utc).strftime('%Y%m%d_%H%M%S')
    return f'/tmp/frame_{stamp}.jpg'
