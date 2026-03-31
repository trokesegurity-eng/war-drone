# Arquitetura Técnica

Fluxo macro:

`Drone/Sensores -> IA Embarcada -> Gateway/API -> Banco de Dados -> Dashboard/App`

Regra central de alertas:
- pH abaixo de 5.5;
- oxigênio dissolvido abaixo do mínimo;
- turbidez acima do limite;
- espécie invasora detectada;
- falha de comunicação.
