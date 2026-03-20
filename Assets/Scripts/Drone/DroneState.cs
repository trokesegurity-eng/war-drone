using System;

namespace SubDrone.Drone
{
    [Serializable]
    public sealed class DroneState
    {
        public float battery;
        public float integrity;
        public int collisions;
        public float elapsedSeconds;

        public void Reset(float maxBattery, float maxIntegrity)
        {
            battery = maxBattery;
            integrity = maxIntegrity;
            collisions = 0;
            elapsedSeconds = 0f;
        }
    }
}
