using System.Collections;
using UnityEngine;

namespace Interactions
{
    public class DoorInteraction : Interactable
    {
        [SerializeField] private float rotationSpeed = 2;

        protected override void Start()
        {
            UseInfo = "Open Door";
        }

        public override void Interact()
        {
            StartCoroutine(OpenDoor());
        }

        private IEnumerator OpenDoor()
        {
            while (transform.localRotation.eulerAngles.y <= 120)
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}