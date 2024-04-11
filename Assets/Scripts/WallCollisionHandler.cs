
using UnityEngine;

namespace SphereGame
{
    public class WallCollisionHandler : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _wallNormal;
        
        private void OnTriggerEnter(Collider other)
        {
            var otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody == null) return;
            
            var incomingVelocity = otherRigidbody.velocity;
            var reflectedVelocity = Vector3.Reflect(incomingVelocity, _wallNormal);

            otherRigidbody.velocity = reflectedVelocity;
        }
    }

}