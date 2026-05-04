using UnityEngine;
using UnityEngine.UI;
using WarAquaDrone.Gameplay.Drone;

namespace WarAquaDrone.UI.Screens
{
    public sealed class MissionHUD : MonoBehaviour
    {
        [SerializeField] private DroneFacade drone;
        [SerializeField] private Slider energyBar;
        [SerializeField] private Text modeText;

        private void Update()
        {
            if (drone == null) return;

            if (energyBar != null) energyBar.value = drone.Energy01;
            if (modeText != null) modeText.text = drone.CurrentModeName;
        }
    }
}
