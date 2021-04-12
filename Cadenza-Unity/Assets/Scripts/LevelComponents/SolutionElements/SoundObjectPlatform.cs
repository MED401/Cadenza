using Event_System;
using Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace LevelComponents.SolutionElements
{
    public class SoundObjectPlatform : Interactable
    {
        public UnityEvent onCorrectPlaceEvent;
        private AudioSource _noSound;

        [SerializeField] private CorrectInstrument correctInstrument;
        [SerializeField] private CorrectPitch correctPitch;
        [SerializeField] private AudioClip noSoundClip;
        [SerializeField] public Transform soundObjectContainer;

        private LevelController levelController;
        public SoundObject CurrentSoundObject { get; set; }
        public bool HasCorrectAudioClip { get; private set; }

        protected override void Start()
        {
            UseInfo = "Play Current Sound";
            _noSound = gameObject.AddComponent<AudioSource>();
            _noSound.spatialBlend = 0.8f;
            _noSound.clip = noSoundClip;

            levelController = GetComponentInParent<LevelController>();
        }

        public void Place(SoundObject soundObject)
        {
            CurrentSoundObject = soundObject;
            if (CurrentSoundObject.aSource.clip == GetCorrectAudioClip())
            {
                HasCorrectAudioClip = true;
                onCorrectPlaceEvent?.Invoke();
                GameEvents.Current.ValidateSolution(levelController.GetInstanceID());
            }
        }

        public AudioClip GetCorrectAudioClip()
        {
            return Resources.Load<AudioClip>("Audio/Sounds/" + correctInstrument + "/" + (int)correctPitch);
        }

        public override void Interact()
        {
            if ((CurrentSoundObject == null)) _noSound.Play();
            
            else CurrentSoundObject.aSource.Play();
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