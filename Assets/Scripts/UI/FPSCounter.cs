using UnityEngine;
using TMPro;

namespace SphereGame
{
    [RequireComponent(typeof(TMP_Text))]
    public class FPSCounter : MonoBehaviour
    {
        private TMP_Text fpsText;

        private void Awake()
        {
            fpsText = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            var fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = $"FPS : {fps:0.}";
        }
    }

}