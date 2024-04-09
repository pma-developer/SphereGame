﻿using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    public class AppStarter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Player _player;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private CompetitorsController _competitorsController;

        private void Start()
        {
            _competitorsController.Init(_gameConfig.CompetitorsCount, _gameConfig.CompetitorMinSize, _gameConfig.CompetitorMaxSize,
                _gameConfig.Gradient);
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            bottomLeft.y = 0;
            var topRight = _camera.ViewportToWorldPoint(new Vector2(1, 1));
            topRight.y = 0;

            var playerTransform = _player.transform;
            _competitorsController.GenerateCompetitors(playerTransform.localScale.y, playerTransform.position, bottomLeft, topRight);
        }

        private void StartGame()
        {
        }
    }
}