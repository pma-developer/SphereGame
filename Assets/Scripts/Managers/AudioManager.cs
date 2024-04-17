using System.Collections;
using UnityEngine;

namespace SphereGame
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioClip _popSound;
        [SerializeField] private AudioSource _audioSourcePrefab;
        [SerializeField] private int _poolSize = 10;

        private AudioSource[] audioSourcePool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializePool();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializePool()
        {
            audioSourcePool = new AudioSource[_poolSize];
            for (var i = 0; i < _poolSize; i++)
            {
                var source = Instantiate(_audioSourcePrefab, transform);
                source.gameObject.SetActive(false);
                audioSourcePool[i] = source;
            }
        }

        public void PlaySoundAtPosition(AudioClip clip, Vector3 position)
        {
            if (clip == null)
            {
                Debug.LogError("Audio clip is null");
                return;
            }

            if (TryGetAudioSource(out var audioSource))
            {
                audioSource.transform.position = position;
                audioSource.clip = clip;
                audioSource.gameObject.SetActive(true);
                audioSource.Play();
                
                StartCoroutine(DisableOnFinish(audioSource));
            }
        }
        public void PlaySoundAtTransform(AudioClip clip, Transform playTransform)
        {
            PlaySoundAtPosition(clip, playTransform.position);
        }
        public void PlayPopSoundAtTransform(Transform playTransform)
        {
            PlaySoundAtTransform(_popSound, playTransform.transform);
        }

        private bool TryGetAudioSource(out AudioSource audioSource)
        {
            foreach (var source in audioSourcePool)
            {
                if (!source.gameObject.activeInHierarchy)
                {
                    audioSource = source;
                    return true;
                }
            }

            audioSource = null;
            return false;
        }

        private IEnumerator DisableOnFinish(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            source.gameObject.SetActive(false);
        }
    }
}