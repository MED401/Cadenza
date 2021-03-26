using Event_System;
using Player;
using UnityEngine;

namespace Interactions
{
    public class Pickup : Interactable
    {
        private Transform playerHand;
        private new Rigidbody rigidbody;

        protected override void Start()
        {
            base.Start();

            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            playerHand = FindObjectOfType<PlayerController>().transform.Find("Camera").transform
                .Find("PickupContainer");

            GameEvents.Current.OnPlace += OnPlace;
            GameEvents.Current.OnDrop += OnDrop;
        }

        private void OnPlace(int id, Plate target)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = true;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            var thisTransform = transform;
            thisTransform.SetParent(target.placementLocation);
            thisTransform.localPosition = Vector3.zero;
            thisTransform.localRotation = Quaternion.Euler(Vector3.zero);

            GameEvents.Current.PlateActivation(target.GetInstanceID());
        }

        public override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = false;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            
            var thisTransform = transform;
            thisTransform.SetParent(playerHand);
            thisTransform.localPosition = Vector3.zero;
            thisTransform.localRotation = Quaternion.Euler(Vector3.zero);
            
            
        }

        public void OnDrop(int id)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = true;
            transform.parent = null;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }
}