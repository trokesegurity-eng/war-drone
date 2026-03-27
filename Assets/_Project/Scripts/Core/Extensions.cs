using UnityEngine;

namespace WarAquaDrone.Core
{
    public static class Extensions
    {
        public static bool HasReached(this Transform from, Transform target, float distance)
        {
            return Vector3.Distance(from.position, target.position) <= distance;
        }
    }
}
