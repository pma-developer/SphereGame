using System;
using System.Collections;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(SphereResizer))]
    public class Player : MonoBehaviour, ISphere
    {
        private SphereResizer _sphereResizer;
        public event Action<float> onRadiusChange;
        
        public float Radius { get; private set; }

        private void Awake()
        {
            _sphereResizer = GetComponent<SphereResizer>();
        }

        public void Init(float startRadius)
        {
            gameObject.transform.localScale = Vector3.zero;
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

        public void Despawn(Action onComplete = null)
        {
            StartCoroutine(DespawnCoroutine(onComplete));
        }

        private IEnumerator DespawnCoroutine(Action onComplete = null)
        {
            _sphereResizer.Resize(0);
            yield return new WaitForSeconds(_sphereResizer.ResizeAnimDuration);
            gameObject.SetActive(false);
            onComplete?.Invoke();
        }
        private void OnDisable()
        {
            onRadiusChange = null;
        }
    }
}