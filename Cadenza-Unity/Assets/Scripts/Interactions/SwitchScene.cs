using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactions
{
    public class SwitchScene : MonoBehaviour
    {
        public int _nextSceneToLoad;
        public AudioSource playSound;

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