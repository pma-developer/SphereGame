using System;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(SphereResizer))]
    public class Player : MonoBehaviour, ISphere
    {
        private SphereResizer _sphereResizer;
        public event Action<float> onRadiusChange;
        
        public float Radius { get; private set; }

        public void Init(float startRadius)
        {
            SetRadius(startRadius);
        }

        private void SetRadius(float newRadius)
        {
            Radius = newRadius;
            _sphereResizer.Resize(newRadius);
            onRadiusChange?.Invoke(newRadius);
        }
        
        public void IncreaseVolume(float radius)
        {
            var newVolume = Radius.GetSphereVolume() + radius.GetSphereVolume();
            SetRadius(newVolume.GetSphereRadius());
        }

        private void Awake()
        {
            _sphereResizer = GetComponent<SphereResizer>();
        }

        private void OnDisable()
        {
            onRadiusChange = null;
        }
    }
}