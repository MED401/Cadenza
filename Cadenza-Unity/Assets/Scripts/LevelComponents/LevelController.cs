using System.Collections.Generic;
using System.Linq;
using LevelComponents.DisplayElements;
using LevelComponents.SolutionElements;
using UnityEngine;

namespace LevelComponents
{
    public class LevelController : MonoBehaviour
    {
        public AudioSource doorSound;
        public SolutionLight[] solutionLights;

        [SerializeField] private SoundObjectPlatform[] soundObjectPlatforms;
        [SerializeField] private Transform exitDoor;
        [SerializeField] private Transform portalActive;
        public List<AudioClip> correctSoundClips;

        private void Start()
        {
            portalActive.gameObject.SetActive(false);
            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>();
            doorSound.spatialBlend = 0.8f;
            solutionLights = GetComponentsInChildren<SolutionLight>();

            for (var i = 0; i < soundObjectPlatforms.Length; i++)
                  correctSoundClips.Add(soundObjectPlatforms[i].correctNote.clip);
        }

        public void ValidateSolution()
        {
            foreach (var soundObjectPlatform in soundObjectPlatforms)
                if (!soundObjectPlatform.Validate())
                    return;

            portalActive.gameObject.SetActive(true);
            exitDoor.gameObject.SetActive(false);
            doorSound.Play();
        }
    }
}