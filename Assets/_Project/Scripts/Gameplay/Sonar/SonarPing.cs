using UnityEngine;

namespace WarAquaDrone.Gameplay.Sonar
{
    public sealed class SonarPing : MonoBehaviour
    {
        [SerializeField] private float speed = 15f;
        [SerializeField] private float maxScale = 20f;

        private void Update()
        {
            transform.localScale += Vector3.one * (speed * Time.deltaTime);
            if (transform.localScale.x >= maxScale) Destroy(gameObject);
        }
    }
}
