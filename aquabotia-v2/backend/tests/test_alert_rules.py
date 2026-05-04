from app.services.rules import evaluate_sensor_thresholds


def test_thresholds_generate_three_alerts() -> None:
    sample = {
        'ph': 5.0,
        'oxigenio_dissolvido': 4.0,
        'turbidez': 40.0,
    }
    alerts = evaluate_sensor_thresholds(sample)
    assert len(alerts) == 3
