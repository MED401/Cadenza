using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactions
{
    public class SwitchToPrevScene : MonoBehaviour
    {
        public int _prevSceneToLoad;

        private void Start()
        {
            _prevSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
        }

        private void OnTriggerEnter(Collider id)
        {
            SceneManager.LoadScene(_prevSceneToLoad);
        }
    }
}