using Event_System;
using Player;
using UnityEngine;

namespace Interactions
{
    public class Pickup : Interactable
    {
        private Transform playerHand;
        private Rigidbody rigidbody;

        protected override void Start()
        {
            base.Start();

            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            playerHand = FindObjectOfType<PlayerController>().transform.Find("Camera").transform
                .Find("PickupContainer");

            GameEvents.Current.ONInteract += OnInteract;
            GameEvents.Current.OnDrop += OnDrop;
        }

        public override void OnInteract(int id)
        {
            if (ID != id) return;

            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            var thisTransform = transform;
            thisTransform.SetParent(playerHand);
            thisTransform.localPosition = Vector3.zero;
            thisTransform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        public void OnDrop(int id)
        {
            if (ID != id) return;

            transform.parent = null;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }
}