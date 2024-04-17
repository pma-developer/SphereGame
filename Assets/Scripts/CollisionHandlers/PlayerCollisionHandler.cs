
using UnityEngine;


namespace SphereGame
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private ISphere _playerSphere;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerSphere = GetComponent<ISphere>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherSphere = other.gameObject.GetComponent<ISphere>();
            var otherRigidbody = other.gameObject.GetComponent<Rigidbody>();

            if (otherSphere == null || otherRigidbody == null) return;
            
            if (otherSphere.Radius > _playerSphere.Radius)
            {
                var collisionNormal = (transform.position.WithY(0) - other.transform.position.WithY(0)).normalized;
                var incomingVelocity = _rigidbody.velocity;
                var otherVelocity = otherRigidbody.velocity;

                var relativeVelocity = incomingVelocity - otherVelocity;
                var reflectedVelocity = Vector3.Reflect(relativeVelocity, collisionNormal);

                var energyTransfer = incomingVelocity.magnitude * 0.5f;
                _rigidbody.velocity = reflectedVelocity.normalized * energyTransfer;

                otherRigidbody.velocity += -collisionNormal * energyTransfer;
            }
        }
    }

}