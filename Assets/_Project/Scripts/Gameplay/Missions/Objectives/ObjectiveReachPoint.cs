using UnityEngine;

namespace WarAquaDrone.Gameplay.Missions.Objectives
{
    public sealed class ObjectiveReachPoint : ObjectiveBase
    {
        [SerializeField] private Transform target;
        [SerializeField] private float completionDistance = 2f;

        public override string Description => "Alcance o ponto marcado";

        private void Update()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player == null || target == null) return;

            if (Vector3.Distance(player.transform.position, target.position) <= completionDistance)
                IsComplete = true;
        }
    }
}
