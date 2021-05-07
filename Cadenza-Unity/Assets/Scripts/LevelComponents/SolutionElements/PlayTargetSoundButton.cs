using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class PlayTargetSoundButton : Interactable
    {
        private bool _isPlaying;
        private LevelController _levelController;
        private IEnumerator _playSoundsRoutine;

        protected override void Start()
        {
            _levelController = GetComponentInParent<LevelController>();
        }

        private void Update()
        {
            UseInfo = _isPlaying ? "Stop Target Sound" : "Play Target Sound";
        }

        public override void Interact()
        {
            if (!_isPlaying)
            {
                _playSoundsRoutine = PlayCorrectSounds(_levelController.soundObjectPlatforms);
                StartCoroutine(_playSoundsRoutine);
            }
            else
            {
                StopCoroutine(_playSoundsRoutine);

                foreach (var soundObjectPlatform in _levelController.soundObjectPlatforms)
                {
                    soundObjectPlatform.audioSource.Stop();
                    soundObjectPlatform.PlayingSound = false;
                }

                _isPlaying = false;
            }
        }

        private IEnumerator PlayCorrectSounds(IReadOnlyList<SoundObjectPlatform> soundPlatforms)
        {
            _isPlaying = true;

            for (var i = 0; i < soundPlatforms.Count; i++)
            {
                if (i > 0)
                {
                    soundPlatforms[i - 1].PlayingSound = false;
                    soundPlatforms[i - 1].audioSource.Stop();
                    soundPlatforms[i - 1].audioSource.clip = null;
                }

                soundPlatforms[i].PlayingSound = true;
                soundPlatforms[i].audioSource.clip = soundPlatforms[i].correctNote.clip;
                soundPlatforms[i].audioSource.Play();

                yield return new WaitForSeconds(2);
            }

            soundPlatforms[soundPlatforms.Count - 1].PlayingSound = false;
            soundPlatforms[soundPlatforms.Count - 1].audioSource.Stop();
            soundPlatforms[soundPlatforms.Count - 1].audioSource.clip = null;
            _isPlaying = false;
        }
    }
}