using UnityEngine;

namespace WarAquaDrone.Gameplay.Sonar
{
    public sealed class SonarTarget : MonoBehaviour
    {
        public enum TargetType { Poi, Collectible, Hazard, Objective }
        public TargetType type = TargetType.Poi;
    }
}
