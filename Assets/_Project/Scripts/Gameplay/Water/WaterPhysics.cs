using UnityEngine;

namespace WarAquaDrone.Gameplay.Water
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class WaterPhysics : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector3 _flow;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            if (_flow != Vector3.zero) _rigidbody.AddForce(_flow, ForceMode.Acceleration);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<WaterCurrentVolume>(out var volume)) _flow += volume.WorldFlow;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<WaterCurrentVolume>(out var volume)) _flow -= volume.WorldFlow;
        }
    }
}
