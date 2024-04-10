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
        [SerializeField] private CompetitorsController _competitorsController;

        private void Start()
        {
            _competitorsController.Init(_gameConfig.CompetitorsCount, _gameConfig.CompetitorMinRadius, _gameConfig.CompetitorMaxRadius,
                _gameConfig.Gradient);
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            bottomLeft.y = 0;
            var topRight = _camera.ViewportToWorldPoint(new Vector2(1, 1));
            topRight.y = 0;

            var player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            
            _competitorsController.SpawnCompetitors(player.Radius, player.transform.position, bottomLeft, topRight);
            player.onRadiusChange += _competitorsController.OnPlayerRadiusChange;
            
            player.Init(_gameConfig.PlayerStartRadius);
        }
        
        private void StartGame()
        {
        }
    }
}