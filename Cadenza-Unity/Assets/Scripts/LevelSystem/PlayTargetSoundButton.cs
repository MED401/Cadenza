using System.Collections;
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
            StartCoroutine(PlayCorrectSounds(levelController.CorrectSoundClips));
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