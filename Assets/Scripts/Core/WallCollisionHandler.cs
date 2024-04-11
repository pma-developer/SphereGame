
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace SphereGame
{
    public class WallCollisionHandler : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _wallNormal;
        private void OnTriggerEnter(Collider other)
        {
            var sphere = other.GetComponent<ISphere>();
            if (sphere == null) return;
            var otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody == null) return;
            
            var wallThickness = Vector3.Scale(_wallNormal, transform.localScale)/2;
            var wallPosition = Vector3.Scale(_wallNormal.Abs(), transform.position);
            var spherePosition = Vector3.Scale(_wallNormal.Abs(), other.transform.position);

            var overlappingDistance = (wallThickness + wallPosition) - (spherePosition + sphere.Radius * -_wallNormal);
            other.transform.position += overlappingDistance;
            //otherRigidbody.velocity += 0.0001f * _wallNormal;
            
            var incomingVelocity = otherRigidbody.velocity;
            var reflectedVelocity = Vector3.Reflect(incomingVelocity, _wallNormal);

            otherRigidbody.velocity = reflectedVelocity;
        }

        private void OnTriggerStay(Collider other)
        {

        }
    }

}