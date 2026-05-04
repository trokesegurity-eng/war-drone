using UnityEngine;

namespace WarAquaDrone.Gameplay.Stealth
{
    public sealed class StealthSystem : MonoBehaviour
    {
        public float CurrentSignature { get; private set; } = 1f;

        public void SetSignature(float signature)
        {
            CurrentSignature = Mathf.Max(0f, signature);
        }
    }
}
