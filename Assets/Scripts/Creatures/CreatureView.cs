using UnityEngine;
using SubDrone.Data;

namespace SubDrone.Creatures
{
    public sealed class CreatureView : MonoBehaviour
    {
        public SpriteRenderer headRenderer;
        public SpriteRenderer bodyRenderer;
        public SpriteRenderer tailRenderer;

        public void Apply(CreatureDefinition definition, CreaturePart head, CreaturePart body, CreaturePart tail)
        {
            ApplyPart(headRenderer, head, definition.bodyTint);
            ApplyPart(bodyRenderer, body, definition.bodyTint);
            ApplyPart(tailRenderer, tail, definition.bodyTint);
        }

        private static void ApplyPart(SpriteRenderer renderer, CreaturePart part, Color tint)
        {
            if (renderer == null)
            {
                return;
            }

            if (part == null)
            {
                renderer.sprite = null;
                return;
            }

            renderer.sprite = part.sprite;
            renderer.color = tint;
            var transformRef = renderer.transform;
            transformRef.localPosition = part.localOffset;
            transformRef.localRotation = Quaternion.Euler(0f, 0f, part.localRotation);
            transformRef.localScale = new Vector3(part.localScale.x, part.localScale.y, 1f);
        }
    }
}
