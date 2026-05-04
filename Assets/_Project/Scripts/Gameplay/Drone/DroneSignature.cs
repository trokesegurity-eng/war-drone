using UnityEngine;

namespace WarAquaDrone.Gameplay.Drone
{
    public sealed class DroneSignature
    {
        private readonly float _baseNoise;
        private readonly float _noiseAtFull;
        private float _mult = 1f;

        public float CurrentNoise { get; private set; }

        public DroneSignature(float baseNoise, float noiseAtFull)
        {
            _baseNoise = Mathf.Max(0f, baseNoise);
            _noiseAtFull = Mathf.Max(_baseNoise, noiseAtFull);
            CurrentNoise = _baseNoise;
        }

        public void SetMultiplier(float mult)
        {
            _mult = Mathf.Max(0.1f, mult);
        }

        public void Tick(float speed01)
        {
            CurrentNoise = Mathf.Lerp(_baseNoise, _noiseAtFull, speed01) * _mult;
        }
    }
}
