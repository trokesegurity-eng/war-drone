using UnityEngine;
using UnityEngine.EventSystems;

namespace SubDrone.Inputs
{
    /// <summary>Joystick UI opcional para mobile.</summary>
    public sealed class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public RectTransform knob;
        public float radius = 80f;

        public Vector2 Value { get; private set; }
        public bool HasInput => Value.sqrMagnitude > 0.0001f;

        private RectTransform _rectTransform;
        private Vector2 _center;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;
            _center = _rectTransform != null ? _rectTransform.anchoredPosition : Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform != null ? _rectTransform.parent as RectTransform : null,
                eventData.position,
                eventData.pressEventCamera,
                out var localPoint);

            var delta = localPoint - _center;
            var clamped = Vector2.ClampMagnitude(delta, radius);
            Value = clamped / radius;

            if (knob != null)
            {
                knob.anchoredPosition = _center + clamped;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector2.zero;
            if (knob != null)
            {
                knob.anchoredPosition = _center;
            }
        }
    }
}
