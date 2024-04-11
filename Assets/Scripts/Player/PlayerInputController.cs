using UnityEngine;

namespace SphereGame
{
    [RequireComponent(typeof(PlayerMovementController))]

    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private float _forceGainFactor = 0.05f;
        [SerializeField] private float _minForceMagnitude = 0f;
        [SerializeField] private float _maxForceMagnitude = 5f;
        [SerializeField] private float _visualArrowLengthMultiplier = 0.5f;

        [SerializeField] private ArrowController _arrow;

        private Camera _camera;
        // TODO: decouple
        private PlayerMovementController _movementController;

        private float _currentForceMagnitude;
        private Vector3 _currentForceDirection;
        private Vector3 CurrentForce => _currentForceDirection * _currentForceMagnitude;

        private bool _inputLocked;

        public void LockInput()
        {
            if(_inputLocked)
                return;
            
            _inputLocked = true;
#if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase is TouchPhase.Moved or TouchPhase.Stationary)
                {
                    OnInputUp();
                }
            }
#else
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                OnInputUp();
            }
#endif
        }
        public void UnlockInput() => _inputLocked = false;
        
        
        private void OnInputDown()
        {
        }

        private void OnInputPressed(Vector3 inputPosition)
        {
            var position = transform.position;

            var viewportPlayerPos3D = _camera.WorldToViewportPoint(position);
            var viewportPlayerPos = new Vector2(viewportPlayerPos3D.x, viewportPlayerPos3D.y);
            var viewportInputPosition = new Vector2(inputPosition.x / Screen.width, inputPosition.y / Screen.height);
            _currentForceMagnitude = Mathf.Min(_currentForceMagnitude + _forceGainFactor, _maxForceMagnitude);
            var forceDirection2D = -(viewportInputPosition - viewportPlayerPos).normalized;
            _currentForceDirection = new Vector3(forceDirection2D.x, 0, forceDirection2D.y);

            _arrow.SetArrowData(position, CurrentForce * _visualArrowLengthMultiplier + position);
            _arrow.ShowArrow();
        }

        private void OnInputUp()
        {
            _movementController.ApplyForce(CurrentForce);

            _currentForceMagnitude = _minForceMagnitude;
            _arrow.HideArrow();
        }

        private void Awake()
        {
            _movementController = GetComponent<PlayerMovementController>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_inputLocked)
                return;

#if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnInputDown();
                        break;
                    case TouchPhase.Moved or TouchPhase.Stationary:
                        OnInputPressed(Input.mousePosition);
                        break;
                    case TouchPhase.Ended:
                        OnInputUp();
                        break;
                }
            }
#else
            if (Input.GetMouseButtonDown(0))
            {
                OnInputDown();
            }

            if (Input.GetMouseButton(0))
            {
                OnInputPressed(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnInputUp();
            }
#endif
        }
    }
}