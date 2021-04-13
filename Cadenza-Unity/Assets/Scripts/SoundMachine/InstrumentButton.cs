using Event_System;
using Interactions;
using UnityEngine;

namespace SoundMachine
{
    public class  InstrumentButton : Interactable, IButton
    {
        [SerializeField] private Instrument instrument;
        private SoundBox soundBox;

        protected override void Start()
        {
            base.Start();

            soundBox = transform.parent.parent.GetComponent<SoundBox>();
        }

        protected override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;
            
            GameEvents.Current.ChangeInstrument(soundBox.GetInstanceID(), "Audio/Sounds/" + instrument);
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