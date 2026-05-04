using UnityEngine;

namespace WarAquaDrone.UI.Screens
{
    public abstract class UIScreen : MonoBehaviour
    {
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }
}
