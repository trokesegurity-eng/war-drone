using UnityEngine;

namespace SubDrone.Data
{
    [CreateAssetMenu(menuName = "SubDrone/Drone Config", fileName = "DroneConfig")]
    public sealed class DroneConfigSO : ScriptableObject
    {
        [Header("Movement")]
        public float thrust = 18f;
        public float maxSpeed = 8f;
        public float turnSpeed = 240f;

        [Header("Resources")]
        public float maxBattery = 120f;
        public float batteryDrainPerSecond = 1f;
        public float batteryDrainThrustMultiplier = 1.8f;

        [Header("Integrity")]
        public float maxIntegrity = 100f;
        public float collisionDamageMultiplier = 6f;
    }
}
