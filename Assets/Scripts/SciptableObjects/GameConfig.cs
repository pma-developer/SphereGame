using System;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _competitorsCount;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private float _competitorMinRadius;
        [SerializeField] private float _competitorMaxRadius;
        [SerializeField] private float _playerStartRadius;

        public int CompetitorsCount => _competitorsCount;
        public Gradient Gradient => _gradient;
        public float CompetitorMinRadius => _competitorMinRadius;
        public float CompetitorMaxRadius => _competitorMaxRadius;
        public float PlayerStartRadius => _playerStartRadius;

        private void LoadFromData(GameConfigData data)
        {
            _competitorsCount = data.CompetitorsCount;
            _gradient = data.Gradient;
            _competitorMinRadius = data.CompetitorMinRadius;
            _competitorMaxRadius = data.CompetitorMaxRadius;
            _playerStartRadius = data.PlayerStartRadius;
        }

        private GameConfigData GetData()
        {
            return new GameConfigData(_competitorsCount, _gradient, _competitorMinRadius, _competitorMaxRadius, _playerStartRadius);
        }

        public void SaveToJson(string path)
        {
            var data = GetData();
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public void LoadFromJson(string path)
        {
            var json = File.ReadAllText(path);
            LoadFromData(JsonUtility.FromJson<GameConfigData>(json));
        }

        [Serializable]
        private class GameConfigData
        {
            [SerializeField] private int _competitorsCount;
            [SerializeField] private Gradient _gradient;
            [SerializeField] private float _competitorMinRadius;
            [SerializeField] private float _competitorMaxRadius;
            [SerializeField] private float _playerStartRadius;

            public  int CompetitorsCount => _competitorsCount;
            public Gradient Gradient => _gradient;
            public float CompetitorMinRadius => _competitorMinRadius;
            public float CompetitorMaxRadius => _competitorMaxRadius;
            public float PlayerStartRadius => _playerStartRadius;

            public GameConfigData(int competitorsCount, Gradient gradient, float competitorMinRadius, float competitorMaxRadius,
                float playerStartRadius)
            {
                _competitorsCount = competitorsCount;
                _gradient = gradient;
                _competitorMinRadius = competitorMinRadius;
                _competitorMaxRadius = competitorMaxRadius;
                _playerStartRadius = playerStartRadius;
            }
        }
    }
}