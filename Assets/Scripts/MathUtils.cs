using UnityEngine;

namespace SphereGame
{
    public static class MathUtils
    {
        public static Vector3 GetRandomVector(Vector3 bottomLeft, Vector3 topRight) =>
            new (
                Random.Range(bottomLeft.x, topRight.x),
                Random.Range(bottomLeft.y, topRight.y),
                Random.Range(bottomLeft.z, topRight.z)
            );
        
        public static Vector3 GetRandomSpherePosition(Vector3 bottomLeft, Vector3 topRight, float sphereRadius)
        {
            var adjustedBottomLeft = bottomLeft + Vector3.one * sphereRadius;
            var adjustedTopRight = topRight - Vector3.one * sphereRadius;

            return GetRandomVector(adjustedBottomLeft, adjustedTopRight);
        }
        public static float SqrDistance(this Vector3 vector, Vector3 other) => (vector - other).sqrMagnitude;

        public static bool IsSpheresColliding(Vector3 pos1, float size1, Vector3 pos2, float size2) =>
            pos1.SqrDistance(pos2) < Mathf.Pow(size1 + size2, 2);
    }
}