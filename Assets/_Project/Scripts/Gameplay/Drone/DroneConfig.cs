using UnityEngine;

namespace WarAquaDrone.Gameplay.Drone
{
    [CreateAssetMenu(menuName = "WAR AquaDrone/DroneConfig")]
    public class DroneConfig : ScriptableObject
    {
        [Header("Movement")]
        public float maxForwardSpeed = 6f;
        public float acceleration = 10f;
        public float turnSpeed = 90f;
        public float verticalSpeed = 3f;

        [Header("Water Drag")]
        public float linearDrag = 1.5f;
        public float angularDrag = 2f;

        [Header("Energy")]
        public float maxEnergy = 100f;
        public float energyDrainPerSecAtFull = 1f;

        [Header("Signature")]
        public float baseNoise = 1f;
        public float noiseAtFullSpeed = 3f;
    }
}
