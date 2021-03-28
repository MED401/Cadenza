using Event_System;
using UnityEngine;

namespace LevelSystem
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelEvents.LevelEvents levelEvents; 
        [SerializeField] private SoundObjectPlatform[] soundObjectPlatforms;
        [SerializeField] private Transform exitDoor;
        public AudioClip[] CorrectSoundClips { get; set; }

        private void Start()
        {
            GameEvents.Current.ONValidateSolution += OnValidateSolution;
            
            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>();
            CorrectSoundClips = new AudioClip[soundObjectPlatforms.Length];

            for (var i = 0; i < soundObjectPlatforms.Length; i++)
                CorrectSoundClips[i] = soundObjectPlatforms[i].GetCorrectAudioClip();
        }

        private void OnValidateSolution(int id)
        {
            if(GetInstanceID() != id) return;
        }
    }
}