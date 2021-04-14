using Event_System;
using LevelComponents.DisplayElements;
using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents
{
    public abstract class LevelEvent : MonoBehaviour
    {
        public NoteScriptableObject correctNote;
        public abstract void Event(NoteScriptableObject note);
    }

    public class LevelController : MonoBehaviour
    {
        public AudioSource doorSound;

        public SolutionLight[] solutionLights;

        [SerializeField] private SoundObjectPlatform[] soundObjectPlatforms;
        [SerializeField] private Transform exitDoor;

        public AudioClip[] CorrectSoundClips { get; set; }

        private void Start()
        {
            GameEvents.Current.ONValidateSolution += OnValidateSolution;

            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>();
            CorrectSoundClips = new AudioClip[soundObjectPlatforms.Length];

            doorSound.spatialBlend = 0.8f;

            solutionLights = GetComponentsInChildren<SolutionLight>();
            for (var i = 0; i < soundObjectPlatforms.Length; i++)
                CorrectSoundClips[i] = soundObjectPlatforms[i].GetCorrectAudioClip();
        }

        private void OnValidateSolution(int id)
        {
            if (GetInstanceID() != id) return;

            foreach (var soundObjectPlatform in soundObjectPlatforms)
                if (!soundObjectPlatform.HasCorrectAudioClip)
                    return;

            exitDoor.gameObject.SetActive(false);
            doorSound.Play();
        }
    }
}