using System;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(SphereResizer))]
    public class Player : MonoBehaviour, IVolumeProvider
    {
        private SphereResizer _sphereResizer;
        public event Action onVolumeChange;
        
        public float Radius { get; private set; }

        public void Init(float startRadius)
        {
            SetRadius(startRadius);
        }

        private void SetRadius(float newRadius)
        {
            Radius = newRadius;
            _sphereResizer.Resize(newRadius);
            onVolumeChange?.Invoke();
        }

        public float GetVolume()
        {
            return 4f / 3f * Mathf.PI * Mathf.Pow(Radius, 3);
        }

        public void IncreaseVolume(float volume)
        {
            var newVolume = GetVolume() + volume;
            SetRadius(Mathf.Pow(3 * newVolume/(4*Mathf.PI), 0.3f));
        }

        private void Awake()
        {
            _sphereResizer = GetComponent<SphereResizer>();
        }
        
    }
}