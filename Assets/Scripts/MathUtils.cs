using UnityEngine;

namespace SphereGame
{
    public static class MathUtils
    {
        // TODO : REFACTOR!!
        public const float FloorY = -0.5f;
        
        public static Vector3 GetRandomVector(Vector3 bottomLeft, Vector3 topRight) =>
            new (
                Random.Range(bottomLeft.x, topRight.x),
                Random.Range(bottomLeft.y, topRight.y),
                Random.Range(bottomLeft.z, topRight.z)
            );
        
        public static Vector3 GetRandomSpherePositionIgnoreY(Vector3 bottomLeft, Vector3 topRight, float sphereRadius)
        {
            var XZAxisVector = new Vector3(1, 0, 1); 
            var adjustedBottomLeft = bottomLeft + XZAxisVector * sphereRadius;
            var adjustedTopRight = topRight - XZAxisVector * sphereRadius;

            return GetRandomVector(adjustedBottomLeft, adjustedTopRight);
        }
        public static Vector3 GetRandomSpherePosition(Vector3 bottomLeft, Vector3 topRight, float sphereRadius)
        {
            var adjustedBottomLeft = bottomLeft + Vector3.one * sphereRadius;
            var adjustedTopRight = topRight - Vector3.one * sphereRadius;

            return GetRandomVector(adjustedBottomLeft, adjustedTopRight);
        }
        public static float SqrDistance(this Vector3 vector, Vector3 other) => (vector - other).sqrMagnitude;

        public static bool IsSpheresColliding(Vector3 pos1, float radius1, Vector3 pos2, float radius2) =>
            pos1.SqrDistance(pos2) < Mathf.Pow(radius1 + radius2, 2);
    }
}