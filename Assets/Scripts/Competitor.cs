using System;
using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(Renderer), typeof(SphereResizer))]
    public class Competitor : MonoBehaviour
    {
        private SphereResizer _sphereResizer;
        private Renderer _renderer;
        
        private Gradient _gradient;

        public float Size { get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _sphereResizer = GetComponent<SphereResizer>();
        }

        private void SetSize(float newSize)
        {
            Size = newSize;
            _sphereResizer.Resize(newSize);
        }

        public void Init(Gradient gradient, float size)
        {
            _gradient = gradient;
            SetSize(size);
        }

        public void SetRelativeToSizeColor(float otherSize)
        {
            var propBlock = new MaterialPropertyBlock();

            _renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", GetColorFromOtherSize(otherSize));
            _renderer.SetPropertyBlock(propBlock);
        }

        // TODO: make more advanced color assignment(should take into account others competitors sizes as well)
        private Color GetColorFromOtherSize(float otherSize)
        {
            return _gradient.Evaluate(Size / otherSize);
        }
    }
}