using System.Collections;
using Event_System;
using LevelSystem;
using Player;
using SoundMachine;
using UnityEngine;

namespace Interactions
{
    public class Pickup : Interactable
    {
        protected bool interactable = true;
        protected Transform playerHand;
        protected new Rigidbody rigidbody;

        protected override void Start()
        {
            base.Start();

            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            playerHand = FindObjectOfType<PlayerController>().transform.Find("Camera").transform
                .Find("PickupContainer");

            GameEvents.Current.ONPlace += OnPlace;
            GameEvents.Current.ONTarget += OnTarget;
            GameEvents.Current.ONDrop += OnDrop;
        }

        protected override void OnTarget(int id)
        {
            if ((GetInstanceID() != id) | !interactable) return;

            outline.enabled = true;
        }

        protected virtual void OnPlace(int id, SoundObjectPlatform target)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = true;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            StartCoroutine(LerpPosition(target.transform.GetChild(0), 0.05f));
        }
        
        protected override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = false;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            StartCoroutine(LerpPosition(playerHand, 0.05f));
        }

        private void OnDrop(int id)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = true;
            transform.parent = null;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            rigidbody.AddForce(playerHand.forward * 200f);
        }

        protected IEnumerator LerpPosition(Transform target, float duration)
        {
            float time = 0;
            interactable = false;
            var startPosition = transform.position;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, target.position, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            var thisTransform = transform;
            thisTransform.SetParent(target);
            thisTransform.localRotation = Quaternion.Euler(Vector3.zero);
            thisTransform.position = target.position;
            interactable = true;
        }
    }
}