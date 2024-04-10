using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    public class AppStarter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private UIService _uiService;
        [SerializeField] private CompetitorsController _competitorsController;
        [SerializeField] private GameAreaController _gameAreaController;

        private void Start()
        {
            _uiService.onScreenClick += RestartGame;
            _competitorsController.Init(_gameConfig.CompetitorsCount, _gameConfig.CompetitorMinRadius, _gameConfig.CompetitorMaxRadius,
                _gameConfig.Gradient);

            var player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            MathUtils.GetWorldScreenBorders(out var bottomLeft, out var topRight, _camera);
                
            _gameAreaController.AdjustToViewportResolution(bottomLeft, topRight);
            _competitorsController.SpawnCompetitors(player.Radius, player.transform.position, bottomLeft, topRight, _gameAreaController.GetFloorY());
            player.onRadiusChange += _competitorsController.OnPlayerRadiusChange;
            
            player.Init(_gameConfig.PlayerStartRadius);
        }

        private void RestartGame()
        {
        }
    }
}