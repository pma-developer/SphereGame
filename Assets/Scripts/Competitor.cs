using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Renderer), typeof(SphereResizer), typeof(CompetitorCollisionHandler))]
    public class Competitor : MonoBehaviour, ISphere
    {
        private SphereResizer _sphereResizer;
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
        }

        public void Init(Gradient gradient, float size)
        {
            _gradient = gradient;
            SetRadius(size);
            _collisionHandler.onCollisionWithBigger += Despawn;
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

            _renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", GetColorFromOtherSize(otherSize));
            _renderer.SetPropertyBlock(propBlock);
        }

        private void Despawn()
        {
            StartCoroutine(DespawnCoroutine());
        }

        private IEnumerator DespawnCoroutine()
        {
            _sphereCollider.enabled = false;
            _sphereResizer.Resize(0);
            _collisionHandler.onCollisionWithBigger -= Despawn;
            yield return new WaitForSeconds(_sphereResizer.ResizeAnimDuration);
            gameObject.SetActive(false);
        }

        // TODO: make more advanced color assignment(should take into account others competitors sizes as well)
        private Color GetColorFromOtherSize(float otherSize)
        {
            return _gradient.Evaluate(Radius / otherSize);
        }
    }
}