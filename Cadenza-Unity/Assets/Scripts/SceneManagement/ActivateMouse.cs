using UnityEngine;

namespace SceneManagement
{
    public class ActivateMouse : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}