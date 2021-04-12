using System.Collections;
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
            StartCoroutine(PlayCorrectSounds(_levelController.CorrectSoundClips));
        }

        private IEnumerator PlayCorrectSounds(AudioClip[] sounds)
        {
            
            var i = 0;
            while (i < sounds.Length)
            {
                if (i > 0)
                {
                    _levelController.solutionLights[i - 1].TurnOff();
                }

                _levelController.solutionLights[i].TurnOn();
                
                _audioSource.clip = sounds[i];
                _audioSource.Play();
                i++;
                yield return new WaitForSeconds(2);
            }
            _audioSource.Stop();
            _levelController.solutionLights[_levelController.solutionLights.Length-1].TurnOff();
        }
    }
}