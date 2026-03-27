using UnityEngine;

namespace WarAquaDrone.InputSystem
{
    public sealed class MobileInputView : MonoBehaviour
    {
        public Vector2 move;
        public float vertical;
        public bool ping;
        public bool stealth;
        public bool mimetic;

        public DroneInput Read()
        {
            var input = new DroneInput
            {
                Move = move,
                Vertical = vertical,
                PingSonar = ping,
                ToggleStealth = stealth,
                ToggleMimetic = mimetic
            };

            ping = false;
            stealth = false;
            mimetic = false;
            return input;
        }
    }
}
