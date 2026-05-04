using UnityEngine;
using WarAquaDrone.InputSystem;

namespace WarAquaDrone.Gameplay.Drone
{
    public sealed class DroneMotor
    {
        private readonly Rigidbody _rigidbody;
        private readonly DroneConfig _config;
        private float _currentForwardSpeed;
        private float _speedMult = 1f;
        private float _maneuverMult = 1f;

        public float ForwardSpeed01 => Mathf.Clamp01(Mathf.Abs(_currentForwardSpeed) / _config.maxForwardSpeed);

        public DroneMotor(Rigidbody rigidbody, DroneConfig config)
        {
            _rigidbody = rigidbody;
            _config = config;
        }

        public void SetMultipliers(float speed, float maneuver)
        {
            _speedMult = speed;
            _maneuverMult = maneuver;
        }

        public void Tick(DroneInput input, float dt)
        {
            var yaw = input.Move.x * _config.turnSpeed * _maneuverMult * dt;
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0f, yaw, 0f));

            var targetSpeed = input.Move.y * _config.maxForwardSpeed * _speedMult;
            _currentForwardSpeed = Mathf.MoveTowards(_currentForwardSpeed, targetSpeed, _config.acceleration * dt);

            var verticalVel = input.Vertical * _config.verticalSpeed;
            _rigidbody.velocity = _rigidbody.transform.forward * _currentForwardSpeed + _rigidbody.transform.up * verticalVel;
        }
    }
}
