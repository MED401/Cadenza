using System.Collections;
using Interactions;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;

namespace LevelComponents.SolutionElements
{
    public class SoundObject : Pickup
    {
        public NoteScriptableObject note;

        [SerializeField] private AudioMixerGroup audioMixerGroup;
        
        private Coroutine _playSoundCoroutine;


        private AudioSource _audioSource;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.spatialBlend = 0.8f;
            _audioSource.outputAudioMixerGroup = audioMixerGroup;
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

            _audioSource.clip = note.clip;
            _playSoundCoroutine = StartCoroutine(PlaySoundRoutine());
        }

        private IEnumerator PlaySoundRoutine()
        {
            _audioSource.Stop();
            _audioSource.Play();
            yield return new WaitForSeconds(2f);
            _audioSource.Stop();
        }
    }
}