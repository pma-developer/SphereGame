using UnityEngine;

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
            Application.targetFrameRate = 144;
            _uiService.onScreenClick += RestartGame;

            MathUtils.GetWorldScreenBorders(out var bottomLeft, out var topRight, _camera);
            _gameAreaController.AdjustToViewportResolution(bottomLeft, topRight);
            _playerSpawnPoint.position = _playerSpawnPoint.position.WithY(_gameAreaController.GetFloorY());
            
            InitCompetitorsController();
            _competitorsController.SpawnCompetitors(_gameConfig.PlayerStartRadius, _playerSpawnPoint.position, bottomLeft, topRight,
                _gameAreaController.GetFloorY());

            _player = Instantiate(_playerPrefab);
            InitPlayer(_player);
        }

        private void InitCompetitorsController()
        {
            _competitorsController.Init(_gameConfig.CompetitorsCount, _gameConfig.CompetitorMinRadius, _gameConfig.CompetitorMaxRadius,
                _gameConfig.Gradient);
            _competitorsController.onPlayerBecameLargest += EndGameVictory;
            _competitorsController.onPlayerBecameSmallest += EndGameLoss;
        }

        private void InitPlayer(Player instantiatedPlayer)
        {
            _player.gameObject.SetActive(true);
            instantiatedPlayer.transform.position = _playerSpawnPoint.position;
            instantiatedPlayer.onRadiusChange += _competitorsController.OnPlayerRadiusChange;
            instantiatedPlayer.Init(_gameConfig.PlayerStartRadius);
        }

        private void EndGameVictory()
        {
            _uiService.ShowVictoryScreen();
            _player.LockInput();
        }
        private void EndGameLoss()
        {
            _uiService.ShowLoseScreen();
            _player.LockInput();
        }

        private void RestartGame()
        {
            _uiService.HideEndGameScreen();

            _competitorsController.DespawnAllCompetitors();
            _player.Despawn(() =>
            {
                MathUtils.GetWorldScreenBorders(out var bottomLeft, out var topRight, _camera);
                _competitorsController.SpawnCompetitors(_gameConfig.PlayerStartRadius, _playerSpawnPoint.position, bottomLeft, topRight,
                    _gameAreaController.GetFloorY());
                InitPlayer(_player);
            });
        }
    }
}