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
        public static Vector3 WithX(this Vector3 vector, float newX) => new (newX, vector.y, vector.z);
        public static Vector3 WithY(this Vector3 vector, float newY) => new (vector.x, newY, vector.z);
        public static Vector3 WithZ(this Vector3 vector, float newZ) => new (vector.x, vector.y, newZ);
        public static Vector3 Abs(this Vector3 vector) => new (Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));

        public static bool IsSpheresColliding(Vector3 pos1, float radius1, Vector3 pos2, float radius2) =>
            pos1.SqrDistance(pos2) < Mathf.Pow(radius1 + radius2, 2);
        
        public static float GetSphereVolume(this float radius) => 4f / 3f * Mathf.PI * Mathf.Pow(radius, 3);
        public static float GetSphereRadius(this float volume) => Mathf.Pow(3 * volume / (4 * Mathf.PI), 1/3f);
        public static void GetWorldScreenBorders(out Vector3 bottomLeft, out Vector3 topRight, Camera camera)
        {
            bottomLeft = camera.ViewportToWorldPoint(new Vector2(0, 0));
            bottomLeft.y = 0;
            topRight = camera.ViewportToWorldPoint(new Vector2(1, 1));
            topRight.y = 0;
        }

    }
}