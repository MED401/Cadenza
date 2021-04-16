using UnityEngine;

namespace PassiveInteractions
{
    public class PressurePlateDoor : MonoBehaviour
    {
        [SerializeField] private GameObject door;
        private bool _isOpened;

        private void OnTriggerEnter(Collider other)
        {
            if (_isOpened) return;
            _isOpened = true;
            door.transform.position += new Vector3(0, -4, 0);
        }
    }
}