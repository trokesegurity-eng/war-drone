using UnityEngine;
using WarAquaDrone.Gameplay.Drone;
using WarAquaDrone.Gameplay.Progression.Items;

namespace WarAquaDrone.UI.Hangar
{
    public sealed class HangarManager : MonoBehaviour
    {
        [SerializeField] private DroneFacade previewDrone;

        public void EquipPart(DronePart part)
        {
            if (previewDrone == null || part == null) return;
            previewDrone.EquipPart(part);
        }

        public void SetStealthMode()
        {
            if (previewDrone == null) return;
            previewDrone.SetMode(DroneMode.Stealth);
        }

        public void SetMimeticMode()
        {
            if (previewDrone == null) return;
            previewDrone.SetMode(DroneMode.Mimetic);
        }
    }
}
