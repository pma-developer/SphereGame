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
    }
}