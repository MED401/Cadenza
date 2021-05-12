using System.Collections;
using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.WinterWonderlandPillars
{
    public class PlayEventTargetSound : Interactable
    {
        [SerializeField] private SoundObjectPlatform soundObjectPlatform;
        private AudioClip _audioClip;

        protected override void Start()
        {
            base.Start();
            UseInfo = "Play Obstacle Activation Sound";
            _audioClip = soundObjectPlatform.GetComponent<LevelEvent>().correctNoteForEvent.clip;
        }

        public override void Interact()
        {
            StartCoroutine(PlayCorrectSounds());
        }

        private IEnumerator PlayCorrectSounds()
        {
            soundObjectPlatform.audioSource.clip = _audioClip;

            soundObjectPlatform.audioSource.Play();
            soundObjectPlatform.PlayingSound = true;

            yield return new WaitForSeconds(2);

            soundObjectPlatform.PlayingSound = false;
            soundObjectPlatform.audioSource.Stop();
        }
    }
}