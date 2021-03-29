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

        [SerializeField] private CorrectInstrument correctInstrument;
        [SerializeField] private CorrectPitch correctPitch;

        private readonly bool interactable = true;
        private SoundObject currentSoundObject;
        private LevelController levelController;
        private Transform soundObjectContainer;

        protected override void Start()
        {
            base.Start();

            GameEvents.Current.ONPlace += OnPlace;
            levelController = GetComponentInParent<LevelController>();
            soundObjectContainer = transform.GetChild(0);
        }

        private void OnPlace(int id, SoundObjectPlatform platform)
        {
            if (platform != this) return;
            currentSoundObject = soundObjectContainer.GetComponentInChildren<SoundObject>();

            if (currentSoundObject.AudioSource.clip == GetCorrectAudioClip()) onCorrectPlaceEvent?.Invoke();
        }

        public bool HasCorrectSoundObject()
        {
            return currentSoundObject.AudioSource.clip == GetCorrectAudioClip();
        }

        public AudioClip GetCorrectAudioClip()
        {
            return Resources.Load<AudioClip>("Audio/Sounds/" + correctInstrument + "/" + (int) correctPitch);
        }

        protected override void OnInteract(int id)
        {
            if ((GetInstanceID() != id) | (currentSoundObject == null) | (interactable != true)) return;
        }

        private enum CorrectInstrument
        {
            Guitar,
            Vocals,
            Oboe,
            Organ,
            SpaceBot,
            Tuba
        }

        private enum CorrectPitch
        {
            Low,
            LowMedium,
            Medium,
            HighMedium,
            High
        }
    }
}