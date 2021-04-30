using System.Collections;
using System.Collections.Generic;
using Interactions;
using LevelComponents.SolutionElements;
using UnityEngine;

namespace LevelComponents
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

        private IEnumerator PlayCorrectSounds(IReadOnlyList<SoundObjectPlatform> soundsPlatforms)
        {
            var i = 0;
            while (i < soundsPlatforms.Count)
            {
                if (i > 0)
                {
                    soundsPlatforms[i - 1].DisableLight();
                    soundsPlatforms[i - 1].audioSource.Stop();
                }

                soundsPlatforms[i].EnableLight();
                soundsPlatforms[i].audioSource.clip = soundsPlatforms[i].correctNote.clip;
                soundsPlatforms[i].audioSource.Play();
                i++;
                yield return new WaitForSeconds(2);
            }

            soundsPlatforms[soundsPlatforms.Count - 1].audioSource.Stop();
            soundsPlatforms[soundsPlatforms.Count - 1].DisableLight();
        }
    }
}