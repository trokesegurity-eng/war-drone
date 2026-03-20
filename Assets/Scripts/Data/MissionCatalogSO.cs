using UnityEngine;

namespace SubDrone.Data
{
    [CreateAssetMenu(menuName = "SubDrone/Mission Catalog", fileName = "MissionCatalog")]
    public sealed class MissionCatalogSO : ScriptableObject
    {
        public MissionDefinition[] missions;

        public MissionDefinition GetFirst(GameModeType mode, BiomeType biome)
        {
            if (missions == null)
            {
                return null;
            }

            foreach (var mission in missions)
            {
                if (mission != null && mission.mode == mode && mission.biome == biome)
                {
                    return mission;
                }
            }

            return null;
        }
    }
}
