using System;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(Collider), typeof(IVolumeProvider))]
    public class CompetitorCollisionHandler : MonoBehaviour
    {
        private IVolumeProvider _volumeProvider;

        public event Action onCollisionWithBigger;
        
        private void Awake()
        {
            _volumeProvider = GetComponent<IVolumeProvider>();
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherVolumeProvider = other.gameObject.GetComponent<IVolumeProvider>();
            if (otherVolumeProvider.GetVolume() > _volumeProvider.GetVolume())
            {
                otherVolumeProvider.IncreaseVolume(_volumeProvider.GetVolume());
                onCollisionWithBigger?.Invoke();
            }
        }
    }
}