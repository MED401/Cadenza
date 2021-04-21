using Interactions;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class LoadOnInteract : Interactable
    {
        public override void Interact()
        {
            SceneManager.LoadScene("Finalize box");
        }
    }
}