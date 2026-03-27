using UnityEngine;

namespace WarAquaDrone.Gameplay.Missions.Objectives
{
    public sealed class ObjectiveCollectItem : ObjectiveBase
    {
        [SerializeField] private int requiredItems = 3;
        private int _collected;

        public override string Description => "Colete os itens da missão";

        public void RegisterCollected()
        {
            _collected++;
            if (_collected >= requiredItems) IsComplete = true;
        }
    }
}
