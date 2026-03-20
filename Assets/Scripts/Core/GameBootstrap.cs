using UnityEngine;
using SubDrone.Data;

namespace SubDrone.Core
{
    /// <summary>Bootstrap mínimo para inicializar modo, bioma e missão.</summary>
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Configs")]
        public GameModeType mode = GameModeType.Explorer;
        public BiomeType biome = BiomeType.River;
        public BiomeConfigSO biomeConfig;
        public DroneConfigSO droneConfig;
        public MissionCatalogSO missionCatalog;

        private void Awake()
        {
            var mission = missionCatalog != null ? missionCatalog.GetFirst(mode, biome) : null;
            var context = FindObjectOfType<GameContext>();
            if (context == null)
            {
                context = new GameObject("GameContext").AddComponent<GameContext>();
            }

            context.SetSession(mode, biome, biomeConfig, droneConfig, mission);
        }
    }
}
