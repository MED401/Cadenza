using System.Collections;
using Event_System;
using Interactions;
using UnityEngine;

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
            GameEvents.Current.ValidateSolution(levelController.GetInstanceID());
        }

        private IEnumerator PlayCorrectSounds(AudioClip[] sounds)
        {
            var i = 0;
            while (i < sounds.Length)
            {
                audioSource.clip = sounds[i];
                audioSource.Play();
                i++;
                yield return new WaitForSeconds(1);
            }
        }
    }
}