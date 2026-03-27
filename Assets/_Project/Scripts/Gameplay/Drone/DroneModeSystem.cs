namespace WarAquaDrone.Gameplay.Drone
{
    public enum DroneMode { Normal, Stealth, Mimetic }

    public sealed class DroneModeSystem
    {
        public DroneMode Current { get; private set; } = DroneMode.Normal;
        public float SpeedMult { get; private set; } = 1f;
        public float NoiseMult { get; private set; } = 1f;
        public float EnergyMult { get; private set; } = 1f;
        public float SonarMult { get; private set; } = 1f;

        public void SetMode(DroneMode mode)
        {
            Current = mode;

            switch (mode)
            {
                case DroneMode.Stealth:
                    SpeedMult = 0.7f;
                    NoiseMult = 0.4f;
                    EnergyMult = 1.2f;
                    SonarMult = 0.8f;
                    break;
                case DroneMode.Mimetic:
                    SpeedMult = 0.85f;
                    NoiseMult = 0.6f;
                    EnergyMult = 1f;
                    SonarMult = 0.5f;
                    break;
                default:
                    SpeedMult = 1f;
                    NoiseMult = 1f;
                    EnergyMult = 1f;
                    SonarMult = 1f;
                    break;
            }
        }
    }
}
