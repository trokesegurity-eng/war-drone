using UnityEngine;
using WarAquaDrone.Gameplay.Drone;
using WarAquaDrone.Gameplay.Progression.Items;

namespace WarAquaDrone.UI.Screens
{
    public sealed class HangarScreen : UIScreen
    {
        [SerializeField] private DroneFacade previewDrone;

        public void EquipPart(DronePart part)
        {
            if (part == null || previewDrone == null) return;
            previewDrone.EquipPart(part);
        }
    }
}
