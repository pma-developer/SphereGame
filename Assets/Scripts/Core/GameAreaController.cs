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
            var leftWallDepth = _leftWall.localScale.x;
            _leftWall.position = new Vector3(bottomLeft.x - leftWallDepth / 2, _leftWall.position.y, (bottomLeft.z + topRight.z) / 2);

            var rightWallDepth = _rightWall.localScale.x;
            _rightWall.position = new Vector3(topRight.x + rightWallDepth / 2, _rightWall.position.y, (bottomLeft.z + topRight.z) / 2);

            var topWallWidth = _topWall.localScale.z;
            _topWall.position = new Vector3((bottomLeft.x + topRight.x) / 2, _topWall.position.y, topRight.z + topWallWidth / 2);

            var bottomWallWidth = _bottomWall.localScale.z;
            _bottomWall.position = new Vector3((bottomLeft.x + topRight.x) / 2, _bottomWall.position.y, bottomLeft.z - bottomWallWidth / 2);
            
            var viewportSize = GetWorldViewportSize(bottomLeft, topRight);
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