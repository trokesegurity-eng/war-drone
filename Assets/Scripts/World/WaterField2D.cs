using UnityEngine;
using SubDrone.Core;

namespace SubDrone.World
{
    /// <summary>Aplica correnteza e drag no Rigidbody2D do drone.</summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class WaterField2D : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            var rigidbody = other.attachedRigidbody;
            if (rigidbody == null)
            {
                return;
            }

            var context = GameContext.Instance;
            if (context == null || context.BiomeConfig == null)
            {
                return;
            }

            rigidbody.drag = context.BiomeConfig.linearDrag;
            rigidbody.angularDrag = context.BiomeConfig.angularDrag;
            rigidbody.AddForce(context.BiomeConfig.currentForce, ForceMode2D.Force);
        }
    }
}
