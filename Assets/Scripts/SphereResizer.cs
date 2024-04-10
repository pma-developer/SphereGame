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
            var position = transform.position;
            var flatPosition = new Vector3(position.x, 0, position.z);
            iTween.MoveTo(gameObject, Vector3.up * (newRadius + MathUtils.FloorY) + flatPosition, resizeAnimDuration);
            iTween.ScaleTo(gameObject, Vector3.one * (newRadius * 2), resizeAnimDuration);
        }
    }
}