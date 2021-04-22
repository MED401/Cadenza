using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace LevelComponents
{
    public class PlayTargetSoundButton : Interactable
    {
        private AudioSource _audioSource;
        private LevelController _levelController;

        protected override void Start()
        {
            UseInfo = "Play Target Sound";
            _levelController = GetComponentInParent<LevelController>();
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public override void Interact()
        {
            StartCoroutine(PlayCorrectSounds(_levelController.correctSoundClips));
        }

        private IEnumerator PlayCorrectSounds(IReadOnlyList<AudioClip> sounds)
        {
            var i = 0;
            while (i < sounds.Count)
            {
                if (i > 0) _levelController.soundObjectPlatforms[i - 1].DisableLight();

                _levelController.soundObjectPlatforms[i].EnableLight();

                _audioSource.clip = sounds[i];
                _audioSource.Play();
                i++;
                yield return new WaitForSeconds(2);
            }

            _audioSource.Stop();
            _levelController.soundObjectPlatforms[_levelController.soundObjectPlatforms.Length - 1].DisableLight();
        }
    }
}