using System.Collections;
using Interactions;
using UnityEngine;

namespace LevelComponents
{
    public class PlayTargetSoundButton : Interactable
    {
        private AudioSource audioSource;
        private LevelController levelController;

        protected override void Start()
        {
            UseInfo = "Play Target Sound";
            levelController = GetComponentInParent<LevelController>();
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public override void Interact()
        {
            StartCoroutine(PlayCorrectSounds(levelController.CorrectSoundClips));
        }

        private IEnumerator PlayCorrectSounds(AudioClip[] sounds)
        {
            
            var i = 0;
            while (i < sounds.Length)
            {
                if (i > 0)
                {
                    levelController.solutionLights[i - 1].TurnOff();
                }

                levelController.solutionLights[i].TurnOn();
                
                audioSource.clip = sounds[i];
                audioSource.Play();
                i++;
                yield return new WaitForSeconds(2);
            }
            audioSource.Stop();
            levelController.solutionLights[levelController.solutionLights.Length-1].TurnOff();
        }
    }
}