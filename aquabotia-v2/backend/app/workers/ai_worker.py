from datetime import datetime, timezone
import random


def run_edge_inference(mission_id: int) -> dict:
    species = random.choice(['tucunare', 'pirarucu', 'tilapia'])
    return {
        'mission_id': mission_id,
        'species_detected': species,
        'confidence': round(random.uniform(0.7, 0.98), 2),
        'timestamp': datetime.now(timezone.utc).isoformat(),
        'frame_url': '/storage/detections/frame_mock.jpg',
    }
