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
                if (i > 0) _levelController.soundObjectPlatforms[i - 1].DisableLight();

                soundsPlatforms[i].EnableLight();
                soundsPlatforms[i].audioSource.clip = soundsPlatforms[i].correctNote.clip;
                soundsPlatforms[i].audioSource.Play();
                i++;
                yield return new WaitForSeconds(2);
                soundsPlatforms[i].audioSource.Stop();
            }

            soundsPlatforms[i].audioSource.Stop();
            _levelController.soundObjectPlatforms[_levelController.soundObjectPlatforms.Length - 1].DisableLight();
        }
    }
}