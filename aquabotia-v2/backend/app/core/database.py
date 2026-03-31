from app.models.alert import Alert
from app.models.mission import Mission
from app.models.sensor_data import SensorData
from app.models.user import User


class InMemoryDB:
    users: list[User] = []
    missions: list[Mission] = []
    sensor_data: list[SensorData] = []
    alerts: list[Alert] = []


db = InMemoryDB()
