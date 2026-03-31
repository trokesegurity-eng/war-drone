class UploadService:
    @staticmethod
    def register_detection(filename: str) -> dict[str, str]:
        return {
            "message": "upload registrado",
            "frame_url": f"/storage/detections/{filename}",
        }
