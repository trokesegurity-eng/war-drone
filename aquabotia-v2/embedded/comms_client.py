import requests

from sensor_reader import read_sensors

API_URL = "http://localhost:8000/sensor-data/"


def send_data(mission_id: int) -> None:
    payload = read_sensors()
    payload["mission_id"] = mission_id
    response = requests.post(API_URL, json=payload, timeout=10)
    print(response.status_code, response.json())


if __name__ == "__main__":
    send_data(1)
