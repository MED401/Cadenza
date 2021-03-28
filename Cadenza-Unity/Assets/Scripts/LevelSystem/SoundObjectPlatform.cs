using Event_System;
using Interactions;
using SoundMachine;
using UnityEngine;

namespace LevelSystem
{
    public class SoundObjectPlatform : Interactable
    {
        [SerializeField] private CorrectInstrument correctInstrument;
        [SerializeField] private CorrectPitch correctPitch;
        private readonly bool interactable = true;

        private SoundObject currentSoundObject;

        protected override void Start()
        {
            base.Start();

            GameEvents.Current.ONValidatePlace += ValidatePlace; 
        }

        private void ValidatePlace(int id, SoundObject soundObject)
        {
            if(GetInstanceID() != id) return;

            currentSoundObject = soundObject;

            if (currentSoundObject.AudioSource.clip == GetCorrectAudioClip())
            {
                
            }
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