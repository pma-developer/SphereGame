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

        private Player _player;
        private void Start()
        {
            _uiService.onScreenClick += RestartGame;

            MathUtils.GetWorldScreenBorders(out var bottomLeft, out var topRight, _camera);
                
            _gameAreaController.AdjustToViewportResolution(bottomLeft, topRight);
            _playerSpawnPoint.position = _playerSpawnPoint.position.WithY(_gameAreaController.GetFloorY());
            
            _competitorsController.Init(_gameConfig.CompetitorsCount, _gameConfig.CompetitorMinRadius, _gameConfig.CompetitorMaxRadius,
                _gameConfig.Gradient);
            _competitorsController.onPlayerBecameLargest += EndGame;
            _competitorsController.SpawnCompetitors(_gameConfig.PlayerStartRadius, _playerSpawnPoint.position, bottomLeft, topRight, _gameAreaController.GetFloorY());
            
            InitPlayer();
        }
        
        private void InitPlayer()
        {
            _player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            _player.onRadiusChange += _competitorsController.OnPlayerRadiusChange;
            _player.Init(_gameConfig.PlayerStartRadius);
        }

        private void EndGame()
        {
            _uiService.ShowVictoryScreen();
        }
        
        private void RestartGame()
        {
            _competitorsController.DespawnAllCompetitors();
            _player.Despawn(() =>
            {
                _uiService.HideVictoryScreen();
                
                MathUtils.GetWorldScreenBorders(out var bottomLeft, out var topRight, _camera);
                _competitorsController.SpawnCompetitors(_gameConfig.PlayerStartRadius, _playerSpawnPoint.position, bottomLeft, topRight, _gameAreaController.GetFloorY());
                
                _player.gameObject.SetActive(true);
                _player.onRadiusChange += _competitorsController.OnPlayerRadiusChange;
                _player.transform.position = _playerSpawnPoint.position;
                _player.Init(_gameConfig.PlayerStartRadius);
            });
        }
    }
}