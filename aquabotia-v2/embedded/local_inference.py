from camera_capture import capture_frame


def run_local_inference() -> dict:
    frame = capture_frame()
    return {'frame': frame, 'species_detected': 'tucunare', 'confidence': 0.81}
