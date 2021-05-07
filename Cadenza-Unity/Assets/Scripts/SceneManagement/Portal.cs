using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private AudioClip portalActivateAudioClip;
        private AudioSource _audioSource;
        private int _nextSceneToLoad;
        private MeshRenderer _portalMeshRenderer;
        private bool _portalOpen = true;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = portalActivateAudioClip;
            _audioSource.spatialBlend = 0.8f;
            _nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            _portalMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider id)
        {
            if (_portalOpen)
            {
                LevelLoadingScreen.LoadingScreen.BeginLoad();
                SceneManager.LoadSceneAsync(_nextSceneToLoad);
            }
        }

        public void OpenPortal()
        {
            if (_portalOpen) return;

            _portalOpen = true;
            _portalMeshRenderer.enabled = true;
            _audioSource.Play();
        }

        public void ClosePortal()
        {
            if (!_portalOpen) return;

            _portalOpen = false;
            _portalMeshRenderer.enabled = false;
        }
    }
}