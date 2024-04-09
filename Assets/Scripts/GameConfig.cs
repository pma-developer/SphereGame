using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _competitorsCount;
        [SerializeField] private Gradient _gradient;
        [FormerlySerializedAs("_competitorMinSize")] [SerializeField] private float _competitorMinRadius;
        [FormerlySerializedAs("_competitorMaxSize")] [SerializeField] private float _competitorMaxRadius;

        public int CompetitorsCount => _competitorsCount;
        public Gradient Gradient => _gradient;
        public float CompetitorMinRadius => _competitorMinRadius;
        public float CompetitorMaxRadius => _competitorMaxRadius;
    }
}