using UnityEngine;

namespace WarAquaDrone.Gameplay.Progression.Items
{
    public enum PartSlot { Hull, Propulsion, Sensors, Camouflage, Utility }

    [CreateAssetMenu(menuName = "WAR AquaDrone/Drone Part")]
    public class DronePart : ScriptableObject
    {
        public string partId;
        public string displayName;
        public PartSlot slot;

        [Header("Stat Modifiers")]
        public float speedMult = 1f;
        public float noiseMult = 1f;
        public float energyMult = 1f;
        public float maneuverMult = 1f;
        public float sonarMult = 1f;

        [Header("Economy")]
        public int priceCredits = 100;
    }
}
