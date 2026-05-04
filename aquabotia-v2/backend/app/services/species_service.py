from app.models.species import Species


class SpeciesService:
    _species: list[Species] = [
        Species(
            id=1,
            nome_comum="Tucunaré",
            nome_cientifico="Cichla ocellaris",
            categoria="peixe",
            nativa_ou_invasora="nativa",
            bioma="Amazônia",
        ),
        Species(
            id=2,
            nome_comum="Pirarucu",
            nome_cientifico="Arapaima gigas",
            categoria="peixe",
            nativa_ou_invasora="monitorar contexto regional",
            bioma="Amazônia",
        ),
    ]

    @classmethod
    def list_all(cls) -> list[Species]:
        return cls._species
