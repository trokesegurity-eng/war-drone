using System;
using System.Collections.Generic;
using WarAquaDrone.Gameplay.Progression.Items;

namespace WarAquaDrone.Gameplay.Drone
{
    public sealed class DroneLoadout
    {
        private readonly Dictionary<PartSlot, DronePart> _parts = new();

        public void Equip(DronePart part) => _parts[part.slot] = part;

        private float Fold(Func<DronePart, float> selector)
        {
            var value = 1f;
            foreach (var part in _parts.Values) value *= selector(part);
            return value;
        }

        public float SpeedMult => Fold(p => p.speedMult);
        public float NoiseMult => Fold(p => p.noiseMult);
        public float EnergyMult => Fold(p => p.energyMult);
        public float ManeuverMult => Fold(p => p.maneuverMult);
        public float SonarMult => Fold(p => p.sonarMult);
    }
}
