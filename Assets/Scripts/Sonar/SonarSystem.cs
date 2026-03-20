using System;
using UnityEngine;
using SubDrone.Inputs;

namespace SubDrone.Sonar
{
    /// <summary>Sonar e scan simples: ping detecta alvos; scan exige manter input.</summary>
    public sealed class SonarSystem : MonoBehaviour
    {
        public InputRouter input;

        [Header("Sonar")]
        public float pingRadius = 10f;
        public LayerMask targetLayer;
        public float pingCooldownSeconds = 2f;

        [Header("Scan")]
        public float scanRadius = 4f;
        public float scanSecondsRequired = 1.2f;

        public event Action<ISonarTarget> OnPingDetected;
        public event Action<ISonarTarget> OnScanCompleted;

        private readonly Collider2D[] _hits = new Collider2D[32];
        private float _pingCooldown;
        private float _scanTimer;
        private ISonarTarget _currentScan;

        private void Update()
        {
            _pingCooldown = Mathf.Max(0f, _pingCooldown - Time.deltaTime);

            if (input != null && input.SonarPingDown && _pingCooldown <= 0f)
            {
                _pingCooldown = pingCooldownSeconds;
                DoPing();
            }

            DoScanTick();
        }

        private void DoPing()
        {
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, pingRadius, _hits, targetLayer);
            for (var i = 0; i < count; i++)
            {
                var target = _hits[i].GetComponentInParent<ISonarTarget>();
                if (target != null)
                {
                    OnPingDetected?.Invoke(target);
                }
            }
        }

        private void DoScanTick()
        {
            if (input == null || !input.ScanHold)
            {
                _scanTimer = 0f;
                _currentScan = null;
                return;
            }

            var count = Physics2D.OverlapCircleNonAlloc(transform.position, scanRadius, _hits, targetLayer);
            ISonarTarget bestTarget = null;
            var bestDistanceSquared = float.MaxValue;

            for (var i = 0; i < count; i++)
            {
                var target = _hits[i].GetComponentInParent<ISonarTarget>();
                if (target == null || !target.CanScan)
                {
                    continue;
                }

                var distanceSquared = (target.Transform.position - transform.position).sqrMagnitude;
                if (distanceSquared < bestDistanceSquared)
                {
                    bestDistanceSquared = distanceSquared;
                    bestTarget = target;
                }
            }

            if (bestTarget == null)
            {
                _scanTimer = 0f;
                _currentScan = null;
                return;
            }

            if (_currentScan != bestTarget)
            {
                _currentScan = bestTarget;
                _scanTimer = 0f;
            }

            _scanTimer += Time.deltaTime;
            if (_scanTimer < scanSecondsRequired)
            {
                return;
            }

            _scanTimer = 0f;
            OnScanCompleted?.Invoke(_currentScan);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, pingRadius);
            Gizmos.DrawWireSphere(transform.position, scanRadius);
        }
    }
}
