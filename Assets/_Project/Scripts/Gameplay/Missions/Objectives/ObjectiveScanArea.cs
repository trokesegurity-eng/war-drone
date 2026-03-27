using UnityEngine;

namespace WarAquaDrone.Gameplay.Missions.Objectives
{
    public sealed class ObjectiveScanArea : ObjectiveBase
    {
        [SerializeField] private float targetScanPercent = 100f;
        private float _scanPercent;

        public override string Description => "Escaneie a área alvo";

        public void RegisterScanProgress(float value)
        {
            _scanPercent = Mathf.Clamp(_scanPercent + value, 0f, 100f);
            if (_scanPercent >= targetScanPercent) IsComplete = true;
        }
    }
}
