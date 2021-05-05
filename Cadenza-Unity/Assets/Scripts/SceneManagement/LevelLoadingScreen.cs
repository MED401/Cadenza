using UnityEngine;

namespace SceneManagement
{
    public class LevelLoadingScreen : MonoBehaviour
    {
        public static LevelLoadingScreen LoadingScreen;
        private Canvas _canvas;

        private void Awake()
        {
            LoadingScreen = this;
        }

        private void Start()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        public void BeginLoad()
        {
            _canvas.enabled = true;
        }
    }
}