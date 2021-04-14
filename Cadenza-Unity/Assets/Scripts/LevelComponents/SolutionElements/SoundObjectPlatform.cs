using Event_System;
using Interactions;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class SoundObjectPlatform : Interactable
    {
        [SerializeField] private NoteScriptableObject sound;
        [SerializeField] private CorrectInstrument correctInstrument;
        [SerializeField] private CorrectPitch correctPitch;
        [SerializeField] private AudioClip noSoundClip;
        [SerializeField] public Transform soundObjectContainer;

        private LevelController _levelController;
        private AudioSource _noSound;
        private LevelEvent[] _events;
        public SoundObject CurrentSoundObject { get; set; }
        public bool HasCorrectAudioClip { get; private set; }

        protected override void Start()
        {
            UseInfo = "Play Current Sound";
            _noSound = gameObject.AddComponent<AudioSource>();
            _noSound.spatialBlend = 0.8f;
            _noSound.clip = noSoundClip;

            _levelController = GetComponentInParent<LevelController>();
            _events = GetComponents<LevelEvent>();
        }

        public void Place(SoundObject soundObject)
        {
            CurrentSoundObject = soundObject;

            foreach (var evnt in _events)
            {
                evnt.Event(soundObject.note);
            }
            
            if (CurrentSoundObject.ASource.clip == GetCorrectAudioClip())
            {
                HasCorrectAudioClip = true;
                GameEvents.Current.ValidateSolution(_levelController.GetInstanceID());
            }
        }

        public AudioClip GetCorrectAudioClip()
        {
            return Resources.Load<AudioClip>("Audio/Sounds/" + correctInstrument + "/" + (int) correctPitch);
        }

        public override void Interact()
        {
            if (CurrentSoundObject == null) _noSound.Play();

            else CurrentSoundObject.ASource.Play();
        }

        private enum CorrectInstrument
        {
            Guitar,
            Vocal,
            Oboe,
            Organ,
            SpaceBot,
            Tuba
        }

        private enum CorrectPitch
        {
            Low = 1,
            LowMedium = 2,
            Medium = 3,
            HighMedium = 4,
            High = 5
        }
    }
}