using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(CompetitorCollisionHandler))]
    public class CompetitorMovementController : MonoBehaviour
    {
        [SerializeField] private float _forceMultiplier = 10;
        [SerializeField] private float _minTimeBetweenForces = 1f;
        [SerializeField] private float _maxTimeBetweenForces = 5f;

        private CompetitorCollisionHandler _collisionHandler;
        private Rigidbody _rigidbody;
        private float _timeBeforeNextForce = 0f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _timeBeforeNextForce = GetRandomForceDelay();
        }

        private void FixedUpdate()
        {
            if (_timeBeforeNextForce <= 0f)
            {
                ApplyRandomForce();
                _timeBeforeNextForce = GetRandomForceDelay();
            }
            else
            {
                _timeBeforeNextForce -= Time.fixedDeltaTime;
            }
        }

        private void ApplyRandomForce()
        {
            var forceDirection2D = Random.insideUnitCircle;
            var forceDirection = new Vector3(forceDirection2D.x, 0, forceDirection2D.y);
            _rigidbody.AddForce(_forceMultiplier * forceDirection);
        }
        
        private float GetRandomForceDelay() => Random.Range(_minTimeBetweenForces, _maxTimeBetweenForces);
    }
}