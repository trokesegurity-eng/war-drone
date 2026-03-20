using SubDrone.Data;

namespace SubDrone.Core
{
    /// <summary>Contexto simples de sessão (modo, bioma e missão atual).</summary>
    public sealed class GameContext : Singleton<GameContext>
    {
        public GameModeType Mode { get; private set; } = GameModeType.Explorer;
        public BiomeType Biome { get; private set; } = BiomeType.River;
        public BiomeConfigSO BiomeConfig { get; private set; }
        public DroneConfigSO DroneConfig { get; private set; }
        public MissionDefinition Mission { get; private set; }

        public void SetSession(GameModeType mode, BiomeType biome, BiomeConfigSO biomeConfig, DroneConfigSO droneConfig, MissionDefinition mission)
        {
            Mode = mode;
            Biome = biome;
            BiomeConfig = biomeConfig;
            DroneConfig = droneConfig;
            Mission = mission;
        }
    }
}
