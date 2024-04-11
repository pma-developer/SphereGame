using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    public class SphereResizer : MonoBehaviour
    {
        [SerializeField] private float resizeAnimDuration;
        public float ResizeAnimDuration => resizeAnimDuration;
        public void Resize(float newRadius)
        {
            iTween.Stop(gameObject);
            iTween.MoveAdd(gameObject, Vector3.up * (newRadius - transform.localScale.x/2), resizeAnimDuration);
            iTween.ScaleTo(gameObject, Vector3.one * (newRadius * 2), resizeAnimDuration);
        }
    }
}