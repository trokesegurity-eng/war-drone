from __future__ import annotations

import math
from datetime import datetime, timezone
from pathlib import Path
from typing import List

import cv2
import numpy as np
from fastapi import FastAPI, File, HTTPException, UploadFile
from fastapi.middleware.cors import CORSMiddleware

APP_ROOT = Path(__file__).resolve().parent
UPLOADS_DIR = APP_ROOT / "uploads"
UPLOADS_DIR.mkdir(parents=True, exist_ok=True)

app = FastAPI(
    title="AgroSwarm AI API",
    version="0.1.0",
    description="MVP backend for AgroSwarm AI with NDVI-style analytics, fleet telemetry and pest anomaly detection.",
)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.get("/")
def root() -> dict:
    return {
        "product": "AgroSwarm AI",
        "status": "ok",
        "timestamp": datetime.now(timezone.utc).isoformat(),
        "docs": "/docs",
    }


@app.get("/health")
def health() -> dict:
    return {"status": "healthy", "service": "api", "uploads_dir": str(UPLOADS_DIR)}


@app.get("/ndvi")
def ndvi_series() -> dict:
    samples = [0.51, 0.54, 0.58, 0.61, 0.64, 0.67, 0.7, 0.73, 0.71, 0.68, 0.66, 0.69]
    return {
        "field": "Fazenda Demo - Talhao Norte",
        "generated_at": datetime.now(timezone.utc).isoformat(),
        "ndvi": samples,
        "average": round(sum(samples) / len(samples), 3),
        "trend": "stable_to_positive",
    }


@app.get("/drones")
def drones() -> dict:
    now = datetime.now(timezone.utc).isoformat()
    return {
        "generated_at": now,
        "fleet": [
            {"id": "AS-01", "lat": -15.7937, "lng": -47.8826, "battery": 82, "status": "surveying"},
            {"id": "AS-02", "lat": -15.7960, "lng": -47.8794, "battery": 67, "status": "mapping"},
            {"id": "AS-03", "lat": -15.7918, "lng": -47.8762, "battery": 58, "status": "returning"},
        ],
    }


@app.post("/analyze/ndvi")
async def analyze_ndvi(file: UploadFile = File(...)) -> dict:
    image = await _read_image(file)
    ndvi_like = _compute_ndvi_like(image)
    health_band = "good" if ndvi_like >= 0.55 else "attention" if ndvi_like >= 0.4 else "critical"
    return {
        "filename": file.filename,
        "ndvi_estimate": round(ndvi_like, 3),
        "health_band": health_band,
        "note": "MVP estimate based on visible-spectrum proxy. Replace with calibrated multispectral workflow for production.",
    }


@app.post("/detect-pests")
async def detect_pests(file: UploadFile = File(...)) -> dict:
    image = await _read_image(file)
    result = _detect_pest_anomalies(image)
    result["filename"] = file.filename
    return result


async def _read_image(file: UploadFile) -> np.ndarray:
    suffix = Path(file.filename or "upload.bin").suffix or ".bin"
    destination = UPLOADS_DIR / f"{datetime.now(timezone.utc).strftime('%Y%m%dT%H%M%S%f')}{suffix}"
    payload = await file.read()
    destination.write_bytes(payload)

    array = np.frombuffer(payload, dtype=np.uint8)
    image = cv2.imdecode(array, cv2.IMREAD_COLOR)
    if image is None:
        raise HTTPException(status_code=400, detail="Arquivo inválido ou formato de imagem não suportado.")
    return image


def _compute_ndvi_like(image: np.ndarray) -> float:
    b, g, r = cv2.split(image.astype(np.float32))
    numerator = g - r
    denominator = g + r + 1e-6
    proxy = numerator / denominator
    normalized = np.clip((proxy + 1.0) / 2.0, 0.0, 1.0)
    vegetation_mask = g > np.percentile(g, 55)
    if np.count_nonzero(vegetation_mask) == 0:
        return float(np.mean(normalized))
    return float(np.mean(normalized[vegetation_mask]))


def _detect_pest_anomalies(image: np.ndarray) -> dict:
    hsv = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)

    green_mask = cv2.inRange(hsv, (25, 35, 35), (95, 255, 255))
    yellow_mask = cv2.inRange(hsv, (10, 45, 45), (34, 255, 255))
    brown_mask = cv2.inRange(hsv, (0, 20, 20), (20, 255, 180))

    lesion_mask = cv2.bitwise_or(yellow_mask, brown_mask)
    lesion_mask = cv2.bitwise_and(lesion_mask, cv2.bitwise_not(green_mask))

    kernel = np.ones((5, 5), dtype=np.uint8)
    cleaned = cv2.morphologyEx(lesion_mask, cv2.MORPH_OPEN, kernel)
    cleaned = cv2.morphologyEx(cleaned, cv2.MORPH_CLOSE, kernel)

    contours, _ = cv2.findContours(cleaned, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    detections: List[dict] = []
    for contour in contours:
        area = cv2.contourArea(contour)
        if area < 250:
            continue
        x, y, w, h = cv2.boundingRect(contour)
        confidence = min(0.98, 0.45 + math.log(area + 1, 10) / 4)
        detections.append(
            {
                "label": "pest_or_stress_candidate",
                "confidence": round(confidence, 3),
                "bbox": {"x": int(x), "y": int(y), "width": int(w), "height": int(h)},
                "area_px": round(area, 1),
            }
        )

    severity = "low"
    if len(detections) >= 6:
        severity = "high"
    elif len(detections) >= 3:
        severity = "medium"

    return {
        "summary": {
            "detections": len(detections),
            "severity": severity,
            "method": "opencv_color_anomaly_heuristic",
            "recommended_next_step": "Validate with agronomist and replace heuristic with trained crop-specific detector for production use.",
        },
        "detections": detections,
    }
