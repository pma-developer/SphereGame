using System;
using System.Dynamic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private readonly Vector3 _invalidSpawnVector = new(-9999, -9999, -9999);

        public event Action onPlayerBecameLargest;
        
        public void Init(int competitorsCount, float competitorMinSize, float competitorMaxSize, Gradient gradient)
        {
            _competitorsCount = competitorsCount;
            _competitorMinSize = competitorMinSize;
            _competitorMaxSize = competitorMaxSize;
            _gradient = gradient;
            _competitors = new Competitor[_competitorsCount];
        }

        public void SpawnCompetitors(float playerRadius, Vector3 playerPosition, Vector3 botLeft, Vector3 rightTop, float floorY)
        {
            for (var i = 0; i < _competitors.Length; i++)
            {
                var randomRadius = Random.Range(_competitorMinSize, _competitorMaxSize);

                if (TryGetValidSpawnPoint(out var spawnPosition, randomRadius, playerRadius, playerPosition,
                        botLeft, rightTop, floorY))
                {
                    var competitor = _competitors[i];
                    if (competitor == null)
                    {
                        var spawnedCompetitor = Instantiate(_competitorPrefab, spawnPosition, Quaternion.identity);
                        _competitors[i] = spawnedCompetitor;
                        competitor = _competitors[i];
                    }
                    else
                    {
                        competitor.gameObject.SetActive(true);
                        competitor.transform.position = spawnPosition;
                    }
                    
                    competitor.Init(_gradient, randomRadius);
                }
            }
        }

        private bool TryGetValidSpawnPoint(out Vector3 resultVector, float radiusToSpawn, float playerRadius, Vector3 playerPosition,
            Vector3 botLeft, Vector3 rightTop, float floorY)
        {
            const int maxPositionGenerationTries = 50;
            resultVector = _invalidSpawnVector;
            for (var j = 0; j < maxPositionGenerationTries; j++)
            {
                var randomInboundsVector = MathUtils.GetRandomSpherePositionIgnoreY(botLeft, rightTop, radiusToSpawn);
                randomInboundsVector = randomInboundsVector.WithY(floorY);
                
                var collidesWithPlayer = MathUtils.IsSpheresColliding(randomInboundsVector, radiusToSpawn, playerPosition, playerRadius);

                if (collidesWithPlayer || HasCollisionsWithCompetitors(randomInboundsVector, radiusToSpawn))
                {
                    continue;
                }

                resultVector = randomInboundsVector;
                break;
            }

            if (resultVector == _invalidSpawnVector)
            {
                Debug.LogError(
                    $"Sphere of size {radiusToSpawn} does not fit into game scene with boundaries {botLeft} : {rightTop}.");
                return false;
            }

            return true;
        }

        private bool HasCollisionsWithCompetitors(Vector3 position, float radius)
        {
            foreach (var competitor in _competitors)
            {
                if (competitor != null &&
                    competitor.gameObject.activeSelf &&
                    MathUtils.IsSpheresColliding(position, radius, competitor.transform.position, competitor.Radius))
                {
                    return true;
                }
            }

            return false;
        }

        public void DespawnAllCompetitors(Action onComplete = null)
        {
            foreach (var competitor in _competitors)
            {
                if (competitor != null && competitor.gameObject.activeSelf)
                {
                    competitor.Despawn();
                }
            }
        }

        public void OnPlayerRadiusChange(float newPlayerRadius)
        {
            var playerIsTheBiggest = true;
            foreach (var competitor in _competitors)
            {
                competitor.SetRelativeToSizeColor(newPlayerRadius);
                if (competitor.Radius > newPlayerRadius)
                {
                    playerIsTheBiggest = false;
                }
            }
            
            if(playerIsTheBiggest)
                onPlayerBecameLargest?.Invoke();
        }
    }
}