using SoundMachine;
using UnityEngine;

namespace LevelSystem
{
    public class SoundObjectPlatform : MonoBehaviour
    {
        private SoundObject currentSoundObject;
        private Transform soundObjectContainer; 

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