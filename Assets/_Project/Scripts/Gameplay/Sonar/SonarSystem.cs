using UnityEngine;

namespace WarAquaDrone.Gameplay.Sonar
{
    public sealed class SonarSystem : MonoBehaviour
    {
        [SerializeField] private float radius = 25f;
        [SerializeField] private LayerMask mask;

        private float _mult = 1f;

        public void SetMultiplier(float multiplier)
        {
            _mult = Mathf.Max(0.1f, multiplier);
        }

        public void Ping()
        {
            var hits = Physics.OverlapSphere(transform.position, radius * _mult, mask, QueryTriggerInteraction.Collide);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<SonarTarget>(out var target))
                    Debug.Log($"[SONAR] {target.type}: {target.name}");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius * _mult);
        }
    }
}
