using UnityEngine;

namespace SubDrone.Prototype3D
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class MantaDroneController3D : MonoBehaviour
    {
        [Header("Movimento")]
        [SerializeField] private float forwardAcceleration = 35f;
        [SerializeField] private float strafeAcceleration = 20f;
        [SerializeField] private float verticalAcceleration = 18f;
        [SerializeField] private float maxSpeed = 22f;
        [SerializeField] private float waterDrag = 1.8f;

        [Header("Rotação")]
        [SerializeField] private float yawSpeed = 70f;
        [SerializeField] private float pitchSpeed = 55f;
        [SerializeField] private float rollVisual = 18f;
        [SerializeField] private float autoLevelSpeed = 2.4f;

        [Header("Boost / Bateria")]
        [SerializeField] private float boostMultiplier = 2.1f;
        [SerializeField] private float maxBattery = 100f;
        [SerializeField] private float batteryDrainIdle = 1.25f;
        [SerializeField] private float batteryDrainBoost = 14f;
        [SerializeField] private float batteryRecharge = 5f;

        [Header("Visual")]
        [SerializeField] private Transform leftWing;
        [SerializeField] private Transform rightWing;
        [SerializeField] private float wingFlapAmplitude = 12f;
        [SerializeField] private float wingFlapFrequency = 3.2f;

        public float BatteryNormalized => maxBattery <= 0f ? 0f : _battery / maxBattery;
        public float Speed => _rb != null ? _rb.velocity.magnitude : 0f;

        private Rigidbody _rb;
        private float _battery;

        public void ConfigureWings(Transform newLeftWing, Transform newRightWing)
        {
            leftWing = newLeftWing;
            rightWing = newRightWing;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.drag = waterDrag;
            _rb.angularDrag = 2.2f;
            _battery = maxBattery;
        }

        private void Update()
        {
            AnimateWings();
            DrainBattery();
        }

        private void FixedUpdate()
        {
            ApplyRotation();
            ApplyThrust();
            ClampSpeed();
            AutoLevel();
        }

        private void ApplyRotation()
        {
            var yaw = Input.GetAxis("Mouse X") + Input.GetAxis("Horizontal") * 0.4f;
            var pitch = -Input.GetAxis("Mouse Y") + Input.GetAxis("Vertical") * 0.25f;

            var yawRotation = Quaternion.AngleAxis(yaw * yawSpeed * Time.fixedDeltaTime, Vector3.up);
            var pitchRotation = Quaternion.AngleAxis(pitch * pitchSpeed * Time.fixedDeltaTime, Vector3.right);
            _rb.MoveRotation(_rb.rotation * yawRotation * pitchRotation);

            var targetRoll = -yaw * rollVisual;
            var currentEuler = _rb.rotation.eulerAngles;
            var currentRoll = Mathf.DeltaAngle(0f, currentEuler.z);
            var newRoll = Mathf.Lerp(currentRoll, targetRoll, Time.fixedDeltaTime * 3.8f);
            var corrected = Quaternion.Euler(currentEuler.x, currentEuler.y, newRoll);
            _rb.MoveRotation(corrected);
        }

        private void ApplyThrust()
        {
            var throttle = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;
            throttle -= Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? 0.65f : 0f;
            var strafe = Input.GetAxis("Horizontal");
            var rise = 0f;
            rise += Input.GetKey(KeyCode.E) ? 1f : 0f;
            rise -= Input.GetKey(KeyCode.Q) ? 1f : 0f;

            var canBoost = _battery > 0.5f;
            var boost = canBoost && (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1));
            var boostFactor = boost ? boostMultiplier : 1f;

            var force = (transform.forward * throttle * forwardAcceleration * boostFactor)
                      + (transform.right * strafe * strafeAcceleration)
                      + (transform.up * rise * verticalAcceleration);

            _rb.AddForce(force, ForceMode.Acceleration);
        }

        private void ClampSpeed()
        {
            if (_rb.velocity.sqrMagnitude <= maxSpeed * maxSpeed)
            {
                return;
            }

            _rb.velocity = _rb.velocity.normalized * maxSpeed;
        }

        private void AutoLevel()
        {
            var up = transform.up;
            var alignment = Vector3.Cross(up, Vector3.up);
            _rb.AddTorque(alignment * autoLevelSpeed, ForceMode.Acceleration);
        }

        private void DrainBattery()
        {
            var boosting = Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1);
            if (boosting && _battery > 0f)
            {
                _battery = Mathf.Max(0f, _battery - batteryDrainBoost * Time.deltaTime);
                return;
            }

            var moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetAxis("Horizontal") != 0f;
            var drain = moving ? batteryDrainIdle : -batteryRecharge;
            _battery = Mathf.Clamp(_battery - drain * Time.deltaTime, 0f, maxBattery);
        }

        private void AnimateWings()
        {
            var flap = Mathf.Sin(Time.time * wingFlapFrequency) * wingFlapAmplitude;

            if (leftWing != null)
            {
                leftWing.localRotation = Quaternion.Euler(0f, 0f, flap);
            }

            if (rightWing != null)
            {
                rightWing.localRotation = Quaternion.Euler(0f, 0f, -flap);
            }
        }
    }
}
