using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactions
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private AudioClip portalActivateAudioClip;
        private AudioSource _audioSource;
        private int _nextSceneToLoad;

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = portalActivateAudioClip;
            _audioSource.spatialBlend = 0.8f;
            _nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        private void OnTriggerEnter(Collider id)
        {
            
            SceneManager.LoadSceneAsync(_nextSceneToLoad);
        }

        public void OpenPortal()
        {
            gameObject.SetActive(true);
            _audioSource.Play();
        }
    }
}