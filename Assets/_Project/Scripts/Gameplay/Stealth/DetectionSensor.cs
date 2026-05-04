using UnityEngine;

namespace WarAquaDrone.Gameplay.Stealth
{
    public sealed class DetectionSensor : MonoBehaviour
    {
        [SerializeField] private float baseRange = 10f;
        [SerializeField] private float detectionThreshold = 2f;

        private void Update()
        {
            var drone = GameObject.FindGameObjectWithTag("Player");
            if (drone == null) return;

            var stealth = drone.GetComponentInChildren<StealthSystem>();
            if (stealth == null) return;

            var signature = stealth.CurrentSignature;
            var range = baseRange * Mathf.Clamp(signature, 0.5f, 4f);
            var distance = Vector3.Distance(transform.position, drone.transform.position);
            var detected = distance <= range && signature >= detectionThreshold;

            if (detected) Debug.DrawLine(transform.position, drone.transform.position, Color.red);
        }
    }
}
