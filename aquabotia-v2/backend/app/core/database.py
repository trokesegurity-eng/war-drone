"""In-memory database for MVP bootstrap."""

DB: dict[str, list[dict]] = {
    'users': [],
    'missions': [],
    'sensor_data': [],
    'species': [],
    'detections': [],
    'alerts': [],
}
