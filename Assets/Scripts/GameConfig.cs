using UnityEngine;

namespace SphereGame
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _competitorsCount;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private float _competitorMinSize;
        [SerializeField] private float _competitorMaxSize;

        public int CompetitorsCount => _competitorsCount;
        public Gradient Gradient => _gradient;
        public float CompetitorMinSize => _competitorMinSize;
        public float CompetitorMaxSize => _competitorMaxSize;
    }
}