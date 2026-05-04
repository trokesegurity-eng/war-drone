from ai.inference.detect_species import detect_species


def run(frame_path: str) -> dict:
    return detect_species(frame_path)
