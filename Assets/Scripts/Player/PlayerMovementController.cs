using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float _forceMultiplier = 10;
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ApplyForce(Vector3 force)
        {
            _rigidbody.AddForce(force*_forceMultiplier);
        }
    }
}