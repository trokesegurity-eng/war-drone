using UnityEngine;

namespace SubDrone.Prototype3D
{
    public sealed class SmoothFollowCamera3D : MonoBehaviour
    {
        public Transform Target;

        [SerializeField] private Vector3 offset = new(0f, 4f, -12f);
        [SerializeField] private float followSpeed = 5f;
        [SerializeField] private float lookAhead = 12f;

        private void LateUpdate()
        {
            if (Target == null)
            {
                return;
            }

            var desired = Target.position + Target.TransformDirection(offset);
            transform.position = Vector3.Lerp(transform.position, desired, Time.deltaTime * followSpeed);

            var lookPoint = Target.position + Target.forward * lookAhead;
            var targetRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * followSpeed);
        }
    }
}
