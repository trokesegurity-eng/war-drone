using UnityEngine;
using UnityEngine.UI;

namespace WarAquaDrone.UI.Widgets
{
    public sealed class Toast : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void Show(string message)
        {
            if (text != null) text.text = message;
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}
