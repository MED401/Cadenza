using Event_System;
using UnityEngine;

namespace SoundMachine
{
    public class InstrumentButton : Button
    {
        [SerializeField] private Instrument instrument = Instrument.Guitar;
        private SoundBox soundBox;

        protected override void Start()
        {
            base.Start();

            soundBox = transform.parent.parent.GetComponent<SoundBox>();
        }

        public override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;
            
            switch (instrument)
            {
                case Instrument.Guitar:
                {
                    GameEvents.Current.ChangeInstrument(soundBox.GetInstanceID(), "Audio/Sounds/Guitar/");
                    break;
                }
                case Instrument.Vocal:
                {
                    GameEvents.Current.ChangeInstrument(soundBox.GetInstanceID(), "Audio/Sounds/Vocals/");
                    break;
                }
                case Instrument.Oboe:
                {
                    GameEvents.Current.ChangeInstrument(soundBox.GetInstanceID(), "Audio/Sounds/Oboe/");
                    break;
                }
                case Instrument.Organ:
                {
                    GameEvents.Current.ChangeInstrument(soundBox.GetInstanceID(), "Audio/Sounds/Organ/");
                    break;
                }
                case Instrument.SpaceBot:
                {
                    GameEvents.Current.ChangeInstrument(soundBox.GetInstanceID(), "Audio/Sounds/SpaceBot/");
                    break;
                }
                case Instrument.Tuba:
                {
                    GameEvents.Current.ChangeInstrument(soundBox.GetInstanceID(), "Audio/Sounds/Tuba/");
                    break;
                }
                default:
                {
                    Debug.Log("Invalid Instrument");
                    break;
                }
            }
        }

        private enum Instrument
        {
            Guitar,
            Vocal,
            Oboe,
            Organ,
            SpaceBot,
            Tuba
        }
    }
}