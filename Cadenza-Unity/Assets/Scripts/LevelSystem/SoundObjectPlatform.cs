using Interactions;
using SoundMachine;
using UnityEngine;

namespace LevelSystem
{
    public class SoundObjectPlatform : Interactable
    {
        [SerializeField] private CorrectInstrument correctInstrument = CorrectInstrument.Guitar;
        [SerializeField] private CorrectPitch correctPitch = CorrectPitch.Low;
        private readonly bool interactable = true;

        private SoundObject currentSoundObject;
        
        protected override void OnInteract(int id)
        {
            if ((GetInstanceID() != id) | (currentSoundObject == null) | (interactable != true)) return;
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