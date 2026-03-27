using UnityEngine;

namespace WarAquaDrone.Gameplay.Missions.Objectives
{
    public abstract class ObjectiveBase : MonoBehaviour
    {
        public bool IsComplete { get; protected set; }
        public abstract string Description { get; }

        public virtual void Activate()
        {
            IsComplete = false;
            enabled = true;
        }

        public virtual void Deactivate() => enabled = false;
    }
}
