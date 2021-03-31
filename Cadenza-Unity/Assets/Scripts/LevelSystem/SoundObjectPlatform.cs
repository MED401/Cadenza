using Event_System;
using Interactions;
using SoundMachine;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSystem
{
    public class SoundObjectPlatform : Interactable
    {
        public UnityEvent onCorrectPlaceEvent;
        private AudioSource NoSound;

        [SerializeField] private CorrectInstrument correctInstrument;
        [SerializeField] private CorrectPitch correctPitch;
        [SerializeField] private AudioClip noSoundClip;

        private LevelController levelController;
        public SoundObject CurrentSoundObject { get; set; }
        public bool HasCorrectAudioClip { get; private set; }

        protected override void Start()
        {
            base.Start();
            NoSound = gameObject.AddComponent<AudioSource>();
            NoSound.spatialBlend = 0.8f;
            NoSound.clip = noSoundClip;

            levelController = GetComponentInParent<LevelController>();
        }

        public void OnPlace(SoundObject soundObject)
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
            return Resources.Load<AudioClip>("Audio/Sounds/" + correctInstrument + "/" + (int) correctPitch);
        }

        protected override void OnInteract(int id)
        {
            if ((GetInstanceID() != id)) return;
            
            if ((CurrentSoundObject == null)) NoSound.Play();
            
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