from app.core.config import settings


def evaluate_sensor_thresholds(data: dict) -> list[dict]:
    triggers: list[dict] = []
    if data['ph'] < settings.alert_ph_min:
        triggers.append({'tipo': 'ph_fora_faixa', 'severidade': 'alta', 'mensagem': 'pH abaixo do mínimo recomendado'})
    if data['oxigenio_dissolvido'] < settings.alert_oxygen_min:
        triggers.append({'tipo': 'oxigenio_baixo', 'severidade': 'media', 'mensagem': 'Oxigênio dissolvido abaixo do mínimo'})
    if data['turbidez'] > settings.alert_turbidity_max:
        triggers.append({'tipo': 'turbidez_alta', 'severidade': 'media', 'mensagem': 'Turbidez acima do limite'})
    return triggers
