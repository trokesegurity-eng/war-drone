from fastapi import FastAPI

from app.api.routes_alerts import router as alerts_router
from app.api.routes_auth import router as auth_router
from app.api.routes_missions import router as missions_router
from app.api.routes_sensors import router as sensors_router
from app.api.routes_species import router as species_router
from app.api.routes_uploads import router as uploads_router
from app.core.config import settings

app = FastAPI(title=settings.app_name, version=settings.app_version)

app.include_router(auth_router, prefix='/auth', tags=['auth'])
app.include_router(missions_router, prefix='/missions', tags=['missions'])
app.include_router(sensors_router, prefix='/sensor-data', tags=['sensor-data'])
app.include_router(species_router, prefix='/species', tags=['species'])
app.include_router(alerts_router, prefix='/alerts', tags=['alerts'])
app.include_router(uploads_router, prefix='/detections', tags=['detections'])


@app.get('/')
def root() -> dict[str, str]:
    return {"project": "Aquabotia", "status": "online", "version": settings.app_version}


@app.get('/health')
def health() -> dict[str, str]:
    return {"api": "ok"}
