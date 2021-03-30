using System.Collections;
using Event_System;
using Interactions;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelSystem
{
    public class PlayTargetSoundButton : Interactable, IButton
    {
        private AudioSource audioSource;
        private LevelController levelController;

        protected override void Start()
        {
            base.Start();

            levelController = GetComponentInParent<LevelController>();
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        protected override void OnInteract(int id)
        {
            if(GetInstanceID() != id) return;
            
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