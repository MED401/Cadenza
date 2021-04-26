using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactions
{
    public class SwitchScene : MonoBehaviour
    {
        public AudioSource playSound;
        private int _nextSceneToLoad;

        private void Start()
        {
            _nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        private void OnTriggerEnter(Collider id)
        {
            playSound.Play();
            SceneManager.LoadScene(_nextSceneToLoad);
        }
    }
}