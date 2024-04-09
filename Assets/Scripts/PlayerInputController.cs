using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

namespace SphereGame
{
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private float _forceGainFactor = 0.05f;
        [SerializeField] private float _minForceMagnitude = 0f;
        [SerializeField] private float _maxForceMagnitude = 5f;
        
        [SerializeField] private float _visualArrowLengthMultiplier = 0.5f;

        [SerializeField] private ArrowController _arrow;
        private PlayerMovementController _movementController;

        private float _currentForceMagnitude;
        private Vector3 _currentForceDirection;
        private Vector3 CurrentForce => _currentForceDirection * _currentForceMagnitude;

        private bool _lmbPressed;

        private void OnLmbDown()
        {
            _arrow.ShowArrow();
        }

        private void OnLmbPressed()
        {
            var position = transform.position;

            var viewportPlayerPos3D = _camera.WorldToViewportPoint(position);
            var viewportPlayerPos = new Vector2(viewportPlayerPos3D.x, viewportPlayerPos3D.y);
            var viewportMousePosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            _currentForceMagnitude = Mathf.Min(_currentForceMagnitude + _forceGainFactor, _maxForceMagnitude);
            var forceDirection2D = -(viewportMousePosition - viewportPlayerPos).normalized;
            _currentForceDirection = new Vector3(forceDirection2D.x, 0, forceDirection2D.y);

            // TODO: draw arrow from sphere boundary, not from center
            _arrow.SetArrowData(position, CurrentForce * _visualArrowLengthMultiplier + position);
        }

        private void OnLmbUp()
        {
            _movementController.ApplyForce(CurrentForce);

            _currentForceMagnitude = _minForceMagnitude;
            _arrow.HideArrow();
        }

        #region MonoBehaviorEvents

        private void Awake()
        {
            _movementController = GetComponent<PlayerMovementController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lmbPressed = true;
                OnLmbDown();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lmbPressed = false;
                OnLmbUp();
            }

            if (_lmbPressed)
            {
                OnLmbPressed();
            }
        }

        #endregion
    }
}