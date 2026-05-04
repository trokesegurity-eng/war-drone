using UnityEngine;

namespace WarAquaDrone.Gameplay.Water
{
    [RequireComponent(typeof(Collider))]
    public sealed class WaterCurrentVolume : MonoBehaviour
    {
        public Vector3 localDirection = Vector3.forward;
        public float strength = 2f;

        public Vector3 WorldFlow => transform.TransformDirection(localDirection.normalized) * strength;

        private void Reset()
        {
            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }
    }
}
