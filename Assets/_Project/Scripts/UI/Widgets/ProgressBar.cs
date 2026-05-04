using UnityEngine;
using UnityEngine.UI;

namespace WarAquaDrone.UI.Widgets
{
    public sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void Set01(float value)
        {
            if (slider != null) slider.value = Mathf.Clamp01(value);
        }
    }
}
