import os
from dataclasses import dataclass


@dataclass(frozen=True)
class Settings:
    app_name: str = os.getenv('AQUABOTIA_APP_NAME', 'Aquabotia API')
    app_version: str = os.getenv('AQUABOTIA_APP_VERSION', '2.1.0')
    jwt_secret: str = os.getenv('AQUABOTIA_JWT_SECRET', 'change-me')
    alert_ph_min: float = float(os.getenv('AQUABOTIA_ALERT_PH_MIN', '5.5'))
    alert_oxygen_min: float = float(os.getenv('AQUABOTIA_ALERT_OXYGEN_MIN', '4.5'))
    alert_turbidity_max: float = float(os.getenv('AQUABOTIA_ALERT_TURBIDITY_MAX', '25.0'))


settings = Settings()
