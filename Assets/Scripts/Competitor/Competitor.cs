using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    [RequireComponent(typeof(Renderer), typeof(SphereResizer), typeof(CompetitorCollisionHandler))]
    public class Competitor : MonoBehaviour, ISphere
    {
        private SphereResizer _sphereResizer;
        private Rigidbody _rigidbody;
        private Renderer _renderer;
        private CompetitorCollisionHandler _collisionHandler;
        private SphereCollider _sphereCollider;
        
        private Gradient _gradient;

        public float Radius { get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _sphereResizer = GetComponent<SphereResizer>();
            _collisionHandler = GetComponent<CompetitorCollisionHandler>();
            _sphereCollider = GetComponent<SphereCollider>();
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        // TODO: validate and dont allow double init with _isSpawned boolean flag 
        public void Init(Gradient gradient, float size)
        {
            gameObject.transform.localScale = Vector3.zero;
            
            _gradient = gradient;
            SetRadius(size);
            _collisionHandler.onCollisionWithBigger += DespawnWithSound;
            _sphereCollider.enabled = true;
            _rigidbody.velocity = Vector3.zero;
        }

        public void IncreaseVolume(float radius)
        {
            var newVolume = Radius.GetSphereVolume() + radius.GetSphereVolume();
            SetRadius(newVolume.GetSphereRadius());
        }
        private void SetRadius(float newRadius)
        {
            Radius = newRadius;
            _sphereResizer.Resize(newRadius);
        }

        public void SetRelativeToSizeColor(float otherSize)
        {
            var propBlock = new MaterialPropertyBlock();

            var color = GetColorFromOtherSize(otherSize);
            _renderer.material.SetColor("_Color", color);
            //_renderer.GetPropertyBlock(propBlock);
            //propBlock.SetColor("_Color", );
            //_renderer.SetPropertyBlock(propBlock);
        }

        private void DespawnWithSound()
        {
            AudioManager.Instance.PlayPopSoundAtTransform(transform);
            Despawn();
        }

        public void Despawn(Action onComplete = null)
        {
            StartCoroutine(DespawnCoroutine(onComplete));
        }

        private IEnumerator DespawnCoroutine(Action onComplete = null)
        {
            _sphereCollider.enabled = false;
            _sphereResizer.Resize(0);
            _collisionHandler.onCollisionWithBigger -= DespawnWithSound;
            
            yield return new WaitForSeconds(_sphereResizer.ResizeAnimDuration);
            
            gameObject.SetActive(false);
            onComplete?.Invoke();
        }

        // TODO: make more advanced color assignment(should take into account others competitors sizes as well)
        private Color GetColorFromOtherSize(float otherSize)
        {
            return _gradient.Evaluate(Radius / otherSize);
        }
    }
}