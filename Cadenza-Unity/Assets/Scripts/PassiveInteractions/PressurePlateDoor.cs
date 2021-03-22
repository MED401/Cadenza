using UnityEngine;

namespace PassiveInteractions
{
    public class PressurePlateDoor : MonoBehaviour
    {
        /*[SerializeField]
        private GameObject door;
        private void Start()
        {
            GameEvents.Current.OnActivateDoor += OnActivate;
        }

        private void OnActivate(int id)
        {
            if (GetInstanceID() != id) return;
            
        }
    }*/
        [SerializeField] private GameObject door;
        private bool isOpened;

        private void OnTriggerEnter(Collider other)
        {
            if (isOpened) return;
            isOpened = true;
            door.transform.position += new Vector3(0, -4, 0);
        }
    }
}