using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    public class SphereResizer : MonoBehaviour
    {
        [SerializeField] private float resizeAnimDuration;

        public void Resize(float newRadius)
        {
            iTween.MoveTo(gameObject, Vector3.up * (newRadius + MathUtils.FloorY) + transform.position, resizeAnimDuration);
            iTween.ScaleTo(gameObject, Vector3.one * (newRadius * 2), resizeAnimDuration);
        }
    }
}