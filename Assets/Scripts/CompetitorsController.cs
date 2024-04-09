using System.Dynamic;
using UnityEngine;

namespace SphereGame
{
    public class CompetitorsController : MonoBehaviour
    {
        [SerializeField] private Competitor _competitorPrefab;
        
        private int _competitorsCount;
        private float _competitorMinSize;
        private float _competitorMaxSize;
        private Gradient _gradient;
        
        private Competitor[] _competitors;
        private readonly Vector3 _invalidSpawnVector = new (-9999, -9999, -9999);

        public void Init(int competitorsCount, float competitorMinSize, float competitorMaxSize, Gradient gradient)
        {
            _competitorsCount = competitorsCount;
            _competitorMinSize = competitorMinSize;
            _competitorMaxSize = competitorMaxSize;
            _gradient = gradient;
            _competitors = new Competitor[_competitorsCount];
        }

        public void GenerateCompetitors(float playerSize, Vector3 playerPosition, Vector3 botLeftBoundary, Vector3 rightTopBoundary)
        {
            for (var i = 0; i < _competitors.Length; i++)
            {
                var randomSize = Random.Range(_competitorMinSize, _competitorMaxSize);

                if (TryGetValidSpawnPoint(out var spawnPosition, randomSize, playerSize, playerPosition, botLeftBoundary, rightTopBoundary))
                {
                    var spawnedCompetitor = Instantiate(_competitorPrefab, spawnPosition, Quaternion.identity);
                    spawnedCompetitor.Init(_gradient, randomSize);
                }
            }
        }

        private bool TryGetValidSpawnPoint(out Vector3 resultVector, float radiusToSpawn, float playerRadius, Vector3 playerPosition, Vector3 botLeftBoundary, Vector3 rightTopBoundary)
        {
            const int maxPositionGenerationTries = 50;
            resultVector = _invalidSpawnVector; 
            for (var j = 0; j < maxPositionGenerationTries; j++)
            {
                var randomInboundsVector = MathUtils.GetRandomSpherePositionIgnoreY(botLeftBoundary, rightTopBoundary, radiusToSpawn);
                    
                if (MathUtils.IsSpheresColliding(randomInboundsVector, radiusToSpawn, playerPosition, playerRadius))
                {
                    continue;
                }

                var collides = false;
                foreach (var competitor in _competitors)
                {
                    if (competitor != null &&
                        MathUtils.IsSpheresColliding(randomInboundsVector, radiusToSpawn, competitor.transform.position, competitor.Radius))
                    {
                        collides = true;
                        break;
                    }
                }

                if (collides)
                {
                    continue;
                }

                resultVector = randomInboundsVector;
            }
            
            if (resultVector == _invalidSpawnVector)
            {
                Debug.LogError($"Sphere of size {radiusToSpawn} does not fit into game scene with boundaries {botLeftBoundary} : {rightTopBoundary}.");
                return false;
            }
            
            return true;
        }

        public void DespawnCompetitor(Competitor competitor)
        {
            competitor.gameObject.SetActive(false);
        }

        public void OnPlayerSizeChange(float newPlayerSize)
        {
            foreach (var competitor in _competitors)
            {
                competitor.SetRelativeToSizeColor(newPlayerSize);
            }
        }
    }
}