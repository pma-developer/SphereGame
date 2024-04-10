using System;
using UnityEngine;
using UnityEngine.UI;

namespace SphereGame
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private Animator _uiAnimator;
        [SerializeField] private Button _fullscreenButton;
        private static readonly int Shown = Animator.StringToHash("Shown");

        public event Action onScreenClick;

        private void OnEnable()
        {
            _fullscreenButton.onClick.AddListener(InvokeOnScreenClick);
        }
        private void OnDisable()
        {
            _fullscreenButton.onClick.RemoveListener(InvokeOnScreenClick);
        }

        private void InvokeOnScreenClick()
        {
            onScreenClick?.Invoke();
        }

        public void ShowVictoryScreen()
        {
            _uiAnimator.SetBool(Shown, true);
        }

        public void HideVictoryScreen()
        {
            _uiAnimator.SetBool(Shown, false);
        }
    }
}