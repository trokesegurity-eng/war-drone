using UnityEngine;
using SubDrone.Core;
using SubDrone.Data;
using SubDrone.Inputs;

namespace SubDrone.Drone
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class DroneController2D : MonoBehaviour
    {
        public InputRouter input;
        public DroneConfigSO overrideConfig;

        public DroneState State { get; } = new();

        private Rigidbody2D _rigidbody;
        private DroneConfigSO _config;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            var context = GameContext.Instance;
            _config = overrideConfig != null ? overrideConfig : context != null ? context.DroneConfig : null;
            if (_config == null)
            {
                Debug.LogError("DroneConfig não definido (overrideConfig ou GameContext).");
                enabled = false;
                return;
            }

            State.Reset(_config.maxBattery, _config.maxIntegrity);
        }

        private void Update()
        {
            if (_config == null)
            {
                return;
            }

            State.elapsedSeconds += Time.deltaTime;

            var move = input != null ? input.Move : Vector2.zero;
            var isThrusting = move.sqrMagnitude > 0.01f;
            var drain = _config.batteryDrainPerSecond * (isThrusting ? _config.batteryDrainThrustMultiplier : 1f);
            State.battery = Mathf.Max(0f, State.battery - drain * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_config == null || State.battery <= 0f)
            {
                return;
            }

            var move = input != null ? input.Move : Vector2.zero;
            var desired = Vector2.ClampMagnitude(move, 1f);
            _rigidbody.AddForce(desired * _config.thrust, ForceMode2D.Force);

            if (_rigidbody.velocity.magnitude > _config.maxSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _config.maxSpeed;
            }

            var turn = input != null ? input.Turn : 0f;
            _rigidbody.AddTorque(-turn * _config.turnSpeed * Mathf.Deg2Rad, ForceMode2D.Force);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_config == null)
            {
                return;
            }

            var damage = collision.relativeVelocity.magnitude * _config.collisionDamageMultiplier;
            if (damage <= 0.5f)
            {
                return;
            }

            State.integrity = Mathf.Max(0f, State.integrity - damage);
            State.collisions++;
        }
    }
}
