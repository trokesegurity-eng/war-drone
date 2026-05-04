using UnityEngine;

namespace WarAquaDrone.App
{
    [CreateAssetMenu(menuName = "WAR AquaDrone/AppConfig")]
    public class AppConfig : ScriptableObject
    {
        public string menuScene = "Menu";
        public string hangarScene = "Hangar";
        public string missionScenePrefix = "Mission_";
    }
}
