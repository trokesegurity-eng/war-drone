using UnityEngine;

namespace WarAquaDrone.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "WAR AquaDrone/MissionDefinition")]
    public class MissionDefinition : ScriptableObject
    {
        public string missionId = "river_01";
        public string sceneName = "Mission_River_01";
        public int rewardXP = 50;
        public int rewardCredits = 100;
    }
}
