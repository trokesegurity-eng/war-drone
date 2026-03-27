using UnityEngine;

namespace WarAquaDrone.InputSystem
{
    public struct DroneInput
    {
        public Vector2 Move;
        public float Vertical;
        public bool PingSonar;
        public bool ToggleStealth;
        public bool ToggleMimetic;
        public bool Ability1;
        public bool Ability2;
    }

    public interface IInputService
    {
        DroneInput Read();
    }

    public sealed class InputService : IInputService
    {
        public DroneInput Read()
        {
            return new DroneInput
            {
                Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")),
                Vertical = (Input.GetKey(KeyCode.E) ? 1f : 0f) + (Input.GetKey(KeyCode.Q) ? -1f : 0f),
                PingSonar = Input.GetKeyDown(KeyCode.Space),
                ToggleStealth = Input.GetKeyDown(KeyCode.Z),
                ToggleMimetic = Input.GetKeyDown(KeyCode.X),
                Ability1 = Input.GetKeyDown(KeyCode.Alpha1),
                Ability2 = Input.GetKeyDown(KeyCode.Alpha2)
            };
        }
    }
}
