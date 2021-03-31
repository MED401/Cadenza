using Interactions;
using LevelSystem;
using UnityEngine;

namespace SoundMachine
{
    public class SoundObject : Pickup
    {
        public AudioSource aSource;

        protected override void Awake()
        {
            base.Awake();
            aSource = gameObject.AddComponent<AudioSource>();
            aSource.spatialBlend = 0.8f;
        }
        
        protected override void OnPlace(int id, SoundObjectPlatform target)
        {
            if (GetInstanceID() != id) return;

            GetComponent<Collider>().enabled = true;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            StartCoroutine(LerpPosition(target.transform.GetChild(0), 0.05f));
            target.OnPlace(this);
        }
    }
}