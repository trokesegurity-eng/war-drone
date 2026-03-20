using UnityEngine;

namespace SubDrone.Core
{
    /// <summary>Singleton simples para serviços de cena (não persistente por padrão).</summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = (T)(object)this;
        }
    }
}
