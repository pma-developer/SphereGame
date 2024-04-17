using UnityEngine;

namespace SphereGame
{
    public class GameAreaController : MonoBehaviour
    {
        [SerializeField] private Transform _leftWall;
        [SerializeField] private Transform _topWall;
        [SerializeField] private Transform _rightWall;
        [SerializeField] private Transform _bottomWall;
        [Space]
        [SerializeField] private Transform _floor;

        public float GetFloorY() => _floor.localScale.y / 2 + _floor.position.y;
        public void AdjustToViewportResolution(Camera currentCamera)
        {
            MathUtils.GetWorldScreenBorders(out var bottomLeft, out var topRight, currentCamera);
            AdjustToViewportResolution(bottomLeft, topRight);
        }
        public void AdjustToViewportResolution(Vector3 bottomLeft, Vector3 topRight)
        {
            var viewportSize = GetWorldViewportSize(bottomLeft, topRight);

            _leftWall.localScale = _leftWall.localScale.WithZ(viewportSize.y);
            _leftWall.position = new Vector3(bottomLeft.x - _leftWall.localScale.x / 2, _leftWall.position.y, (bottomLeft.z + topRight.z) / 2);

            _rightWall.localScale = _rightWall.localScale.WithZ(viewportSize.y);
            _rightWall.position = new Vector3(topRight.x + _rightWall.localScale.x / 2, _rightWall.position.y, (bottomLeft.z + topRight.z) / 2);

            _topWall.localScale = _topWall.localScale.WithX(viewportSize.x);
            _topWall.position = new Vector3((bottomLeft.x + topRight.x) / 2, _topWall.position.y, topRight.z + _topWall.localScale.z / 2);

            _bottomWall.localScale = _bottomWall.localScale.WithX(viewportSize.x);
            _bottomWall.position = new Vector3((bottomLeft.x + topRight.x) / 2, _bottomWall.position.y, bottomLeft.z - _bottomWall.localScale.z / 2);
            
            _floor.localScale = new Vector3(viewportSize.x, _floor.localScale.y, viewportSize.y);
        }
        private Vector2 GetWorldViewportSize(Vector3 bottomLeft, Vector3 topRight)
        {
            var width = topRight.x - bottomLeft.x;
            var height = topRight.z - bottomLeft.z;

            return new Vector2(width, height);
        }
    }
}