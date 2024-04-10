using System;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(Collider), typeof(ISphere))]
    public class CompetitorCollisionHandler : MonoBehaviour
    {
        private ISphere _sphere;

        public event Action onCollisionWithBigger;
        
        private void Awake()
        {
            _sphere = GetComponent<ISphere>();
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherSphere = other.gameObject.GetComponent<ISphere>();
            if (otherSphere.Radius > _sphere.Radius)
            {
                otherSphere.IncreaseVolume(_sphere.Radius);
                onCollisionWithBigger?.Invoke();
            }
        }
    }
}