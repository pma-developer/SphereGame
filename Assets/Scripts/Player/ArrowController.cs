using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(LineRenderer))]
    public class ArrowController : MonoBehaviour
    {
        private const int _positionsCount = 5;
        
        private LineRenderer _lineRenderer;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.positionCount = _positionsCount;
        }

        public void SetArrowData(Vector3 start, Vector3 end, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            var direction = (end - start).normalized;

            var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
            var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;

            _lineRenderer.SetPosition(0, start);
            _lineRenderer.SetPosition(1, end);
            _lineRenderer.SetPosition(2, end + right * arrowHeadLength);
            _lineRenderer.SetPosition(3, end + left * arrowHeadLength);
            _lineRenderer.SetPosition(4, end);
        }

        public void ShowArrow()
        {
            _lineRenderer.enabled = true;
        }
        public void HideArrow()
        {
            _lineRenderer.enabled = false;
        }
    }
}