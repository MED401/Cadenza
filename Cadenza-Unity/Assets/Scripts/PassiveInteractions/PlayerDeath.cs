using UnityEngine;

namespace PassiveInteractions
{
    public class PlayerDeath : MonoBehaviour
    {
        public Transform playerTransform;
        private void OnTriggerEnter(Collider other) {
            playerTransform.position = Vector3.zero;
        }
    }
}
