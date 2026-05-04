using UnityEngine;

namespace WarAquaDrone.Gameplay.Drone
{
    public sealed class DroneEnergy
    {
        private readonly float _drainAtFull;
        private float _mult = 1f;

        public float Current { get; private set; }
        public float Max { get; }

        public DroneEnergy(float max, float drainAtFull)
        {
            Max = Mathf.Max(1f, max);
            Current = Max;
            _drainAtFull = Mathf.Max(0f, drainAtFull);
        }

        public void SetMultiplier(float mult)
        {
            _mult = Mathf.Max(0.1f, mult);
        }

        public void Tick(float speed01, float dt)
        {
            var drain = _drainAtFull * speed01 * dt / _mult;
            Current = Mathf.Clamp(Current - drain, 0f, Max);
        }
    }
}
