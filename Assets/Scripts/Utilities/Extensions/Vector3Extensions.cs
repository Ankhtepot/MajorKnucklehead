using UnityEngine;

namespace Utilities.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 ZToZero(this Vector3 source)
        {
            source.z = 0;
            return source;
        }
    }
}