using UnityEngine;
using WarAquaDrone.Gameplay.Progression.Items;
using WarAquaDrone.Gameplay.Sonar;
using WarAquaDrone.Gameplay.Stealth;
using WarAquaDrone.InputSystem;

namespace WarAquaDrone.Gameplay.Drone
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class DroneFacade : MonoBehaviour
    {
        [SerializeField] private DroneConfig config;

        private DroneMotor _motor;
        private DroneEnergy _energy;
        private DroneSignature _signature;
        private DroneLoadout _loadout;
        private DroneModeSystem _mode;
        private SonarSystem _sonar;
        private StealthSystem _stealth;
        private IInputService _input;

        public float Energy01 => _energy.Current / _energy.Max;
        public string CurrentModeName => _mode.Current.ToString();

        private void Awake()
        {
            var rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.drag = config.linearDrag;
            rb.angularDrag = config.angularDrag;

            _motor = new DroneMotor(rb, config);
            _energy = new DroneEnergy(config.maxEnergy, config.energyDrainPerSecAtFull);
            _signature = new DroneSignature(config.baseNoise, config.noiseAtFullSpeed);
            _loadout = new DroneLoadout();
            _mode = new DroneModeSystem();
            _sonar = GetComponentInChildren<SonarSystem>();
            _stealth = GetComponentInChildren<StealthSystem>();
            _input = new InputService();

            ApplyStats();
        }

        private void Update()
        {
            var input = _input.Read();

            if (input.ToggleStealth) _mode.SetMode(DroneMode.Stealth);
            if (input.ToggleMimetic) _mode.SetMode(DroneMode.Mimetic);

            _motor.Tick(input, Time.deltaTime);
            _energy.Tick(_motor.ForwardSpeed01, Time.deltaTime);
            _signature.Tick(_motor.ForwardSpeed01);

            _stealth?.SetSignature(_signature.CurrentNoise);

            if (input.PingSonar) _sonar?.Ping();
        }

        public void EquipPart(DronePart part)
        {
            _loadout.Equip(part);
            ApplyStats();
        }

        public void SetMode(DroneMode mode)
        {
            _mode.SetMode(mode);
            ApplyStats();
        }

        private void ApplyStats()
        {
            _motor.SetMultipliers(_loadout.SpeedMult * _mode.SpeedMult, _loadout.ManeuverMult);
            _energy.SetMultiplier(_loadout.EnergyMult * _mode.EnergyMult);
            _signature.SetMultiplier(_loadout.NoiseMult * _mode.NoiseMult);
            _sonar?.SetMultiplier(_loadout.SonarMult * _mode.SonarMult);
        }
    }
}
