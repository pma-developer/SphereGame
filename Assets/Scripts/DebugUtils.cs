using UnityEngine;

namespace SphereGame
{
    public static class DebugUtils
    {
        public static void DrawArrow(Vector3 start, Vector3 end, Color arrowColor, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Debug.DrawLine(start, end, arrowColor);
            var direction = (end - start).normalized;

            var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
            var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;

            Debug.DrawLine(end, end + right * arrowHeadLength, arrowColor);
            Debug.DrawLine(end, end + left * arrowHeadLength, arrowColor);
        }
    }
}