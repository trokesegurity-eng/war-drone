using UnityEngine;

namespace SubDrone.Inputs
{
    /// <summary>Input unificado PC e mobile.</summary>
    public sealed class InputRouter : MonoBehaviour
    {
        [Header("Mobile")]
        public VirtualJoystick joystick;

        [Tooltip("Se true, touch na tela controla direção a partir do centro.")]
        public bool touchDragControl = true;

        public Vector2 Move { get; private set; }
        public float Turn { get; private set; }
        public bool SonarPingDown { get; private set; }
        public bool ScanHold { get; private set; }

        private Vector2 _touchOrigin;
        private int _touchId = -1;

        private void Update()
        {
            SonarPingDown = false;
            ScanHold = false;

            var keyboardMove = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
            var keyboardTurn = UnityEngine.Input.GetAxisRaw("Horizontal");

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                SonarPingDown = true;
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetMouseButton(0))
            {
                ScanHold = true;
            }

            if (joystick != null && joystick.HasInput)
            {
                Move = joystick.Value;
                Turn = joystick.Value.x;
                return;
            }

            if (touchDragControl && TryReadTouchDrag(out var touchMove))
            {
                Move = touchMove;
                Turn = touchMove.x;

                for (var i = 0; i < UnityEngine.Input.touchCount; i++)
                {
                    var touch = UnityEngine.Input.GetTouch(i);
                    if (touch.tapCount >= 2 && touch.phase == TouchPhase.Began)
                    {
                        SonarPingDown = true;
                    }

                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        ScanHold = true;
                    }
                }

                return;
            }

            Move = Vector2.ClampMagnitude(keyboardMove, 1f);
            Turn = Mathf.Clamp(keyboardTurn, -1f, 1f);
        }

        private bool TryReadTouchDrag(out Vector2 move)
        {
            move = Vector2.zero;
            if (UnityEngine.Input.touchCount <= 0)
            {
                return false;
            }

            Touch? activeTouch = null;
            for (var i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                var touch = UnityEngine.Input.GetTouch(i);
                if (_touchId < 0 && touch.phase == TouchPhase.Began)
                {
                    _touchId = touch.fingerId;
                    _touchOrigin = touch.position;
                }

                if (touch.fingerId == _touchId)
                {
                    activeTouch = touch;
                    break;
                }
            }

            if (activeTouch == null)
            {
                return false;
            }

            var currentTouch = activeTouch.Value;
            if (currentTouch.phase == TouchPhase.Ended || currentTouch.phase == TouchPhase.Canceled)
            {
                _touchId = -1;
                return false;
            }

            var delta = currentTouch.position - _touchOrigin;
            var normalized = delta / Mathf.Max(120f, Screen.dpi * 0.6f);
            move = Vector2.ClampMagnitude(normalized, 1f);
            return true;
        }
    }
}
