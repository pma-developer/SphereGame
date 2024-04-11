using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SphereGame
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private TMP_Text _endgameMessage;
        [SerializeField] private Animator _uiAnimator;
        [SerializeField] private Button _fullscreenButton;
        private static readonly int Shown = Animator.StringToHash("Shown");

        public event Action onScreenClick;

        private void OnEnable()
        {
            _fullscreenButton.onClick.AddListener(InvokeOnScreenClick);
            _fullscreenButton.gameObject.SetActive(false);
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
            ShowEndGameScreen("YOU WON!");
        }
        public void ShowLoseScreen()
        {
            ShowEndGameScreen("YOU LOST!");
        }

        private void ShowEndGameScreen(string message)
        {
            _endgameMessage.text = message;
            _uiAnimator.SetBool(Shown, true);
            _fullscreenButton.gameObject.SetActive(true);
        }

        public void HideEndGameScreen()
        {
            _uiAnimator.SetBool(Shown, false);
            _fullscreenButton.gameObject.SetActive(false);
        }
    }
}