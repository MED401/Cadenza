using System.Collections;
using Interactions;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class SoundObject : Pickup
    {
        public NoteScriptableObject note;
        private Coroutine _playSoundCoroutine;

        public AudioSource ASource { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            ASource = gameObject.AddComponent<AudioSource>();
            ASource.spatialBlend = 0.8f;
        }

        public override void Place(SoundObjectPlatform target)
        {
            if (target.soundObjectContainer.childCount > 0) return;
            GetComponent<Collider>().enabled = true;
            Rigidbody.isKinematic = true;
            Rigidbody.useGravity = false;

            StartCoroutine(LerpPosition(target.transform.GetChild(0), 0.05f));
            target.OnPlace(this);
        }

        public void PlaySound()
        {
            if (_playSoundCoroutine != null) StopCoroutine(_playSoundCoroutine);

            ASource.clip = note.clip;
            _playSoundCoroutine = StartCoroutine(PlaySoundRoutine());
        }

        private IEnumerator PlaySoundRoutine()
        {
            ASource.Stop();
            ASource.Play();
            yield return new WaitForSeconds(2f);
            ASource.Stop();
        }
    }
}