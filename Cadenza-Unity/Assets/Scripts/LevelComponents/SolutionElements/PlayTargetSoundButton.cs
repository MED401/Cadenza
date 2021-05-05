using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class PlayTargetSoundButton : Interactable
    {
        private LevelController _levelController;

        protected override void Start()
        {
            UseInfo = "Play Target Sound";
            _levelController = GetComponentInParent<LevelController>();
        }

        public override void Interact()
        {
            StartCoroutine(PlayCorrectSounds(_levelController.soundObjectPlatforms));
        }

        private static IEnumerator PlayCorrectSounds(IReadOnlyList<SoundObjectPlatform> soundPlatforms)
        {
            for (var i = 0; i < soundPlatforms.Count; i++)
            {
                if (i > 0)
                {
                    soundPlatforms[i - 1].DisableLight();
                    soundPlatforms[i - 1].audioSource.Stop();
                    soundPlatforms[i - 1].audioSource.clip = null;
                }

                soundPlatforms[i].EnableLight();
                soundPlatforms[i].audioSource.clip = soundPlatforms[i].correctNote.clip;
                soundPlatforms[i].audioSource.maxDistance = 100;
                soundPlatforms[i].audioSource.rolloffMode = AudioRolloffMode.Linear;
                soundPlatforms[i].audioSource.Play();

                yield return new WaitForSeconds(2);
            }

            soundPlatforms[soundPlatforms.Count - 1].DisableLight();
            soundPlatforms[soundPlatforms.Count - 1].audioSource.Stop();
            soundPlatforms[soundPlatforms.Count - 1].audioSource.clip = null;
        }
    }
}