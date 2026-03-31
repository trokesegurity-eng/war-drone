import random
from datetime import datetime, UTC


def read_sensors() -> dict[str, float | str]:
    return {
        "temperatura": round(random.uniform(22.0, 30.0), 2),
        "ph": round(random.uniform(6.0, 8.5), 2),
        "turbidez": round(random.uniform(1.0, 30.0), 2),
        "oxigenio_dissolvido": round(random.uniform(4.0, 10.0), 2),
        "profundidade": round(random.uniform(1.0, 20.0), 2),
        "timestamp": datetime.now(UTC).isoformat(),
    }


if __name__ == "__main__":
    print(read_sensors())
