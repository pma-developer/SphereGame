using UnityEngine;
using UnityEngine.Serialization;

namespace SphereGame
{
    public class SphereResizer : MonoBehaviour
    {
        [SerializeField] private float resizeAnimDuration;
        
        public void Resize(float newSize)
        {
            //gameObject.transform.position = Vector3.up * (newSize / 2 - 0.5f) + transform.position;
            //gameObject.transform.localScale = Vector3.one * newSize;
            
            //iTween.MoveTo(gameObject, Vector3.up * newSize/2 + transform.position, resizeAnimDuration);
            //iTween.ScaleTo(gameObject, Vector3.one * newSize, resizeAnimDuration);
        }
    }
}