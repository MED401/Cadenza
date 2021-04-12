using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements
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


        public override void Place(SoundObjectPlatform target)
        {
            if (target.soundObjectContainer.childCount > 0) return;
            GetComponent<Collider>().enabled = true;
            Rigidbody.isKinematic = true;
            Rigidbody.useGravity = false;

            StartCoroutine(LerpPosition(target.transform.GetChild(0), 0.05f));
            target.Place(this);
        }
    }
}