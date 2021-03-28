using Interactions;
using LevelSystem;
using UnityEngine;

namespace SoundMachine
{
    public class SoundObject : Pickup
    {
        public AudioSource AudioSource { get; set; }

        protected override void Start()
        {
            base.Start();
            AudioSource = gameObject.AddComponent<AudioSource>();
        }

        protected override void OnPlace(int id, SoundObjectPlatform target)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = true;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            StartCoroutine(LerpPosition(target.transform.GetChild(0), 0.05f));
        }
    }
}