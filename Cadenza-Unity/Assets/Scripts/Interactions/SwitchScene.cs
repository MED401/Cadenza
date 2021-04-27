using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactions
{
    public class SwitchScene : MonoBehaviour
    {
        private int _nextSceneToLoad;

        private void Start()
        {
            _nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        private void OnTriggerEnter(Collider id)
        {
            SceneManager.LoadScene(_nextSceneToLoad);
        }
    }
}