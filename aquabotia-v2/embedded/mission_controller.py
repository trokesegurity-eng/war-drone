MISSION_STATUS = {'running': False}


def start_mission() -> dict:
    MISSION_STATUS['running'] = True
    return MISSION_STATUS.copy()


def finish_mission() -> dict:
    MISSION_STATUS['running'] = False
    return MISSION_STATUS.copy()
