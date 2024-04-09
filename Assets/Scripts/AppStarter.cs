using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    public class AppStarter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerInputController _playerInputController;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private CompetitorsController _competitorsController;

        private void Start()
        {
            _competitorsController.Init(_gameConfig.CompetitorsCount, _gameConfig.CompetitorMinRadius, _gameConfig.CompetitorMaxRadius,
                _gameConfig.Gradient);
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            bottomLeft.y = 0;
            var topRight = _camera.ViewportToWorldPoint(new Vector2(1, 1));
            topRight.y = 0;

            var playerTransform = _playerInputController.transform;
            _competitorsController.GenerateCompetitors(playerTransform.localScale.y/2, playerTransform.position, bottomLeft, topRight);
            _competitorsController.OnPlayerSizeChange(playerTransform.localScale.y/2);
        }

        private void StartGame()
        {
        }
    }
}