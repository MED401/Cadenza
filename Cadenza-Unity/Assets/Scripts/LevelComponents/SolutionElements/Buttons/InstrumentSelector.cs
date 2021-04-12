using Event_System;
using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements.Buttons
{
    public class InstrumentSelector : Interactable
    {
        private SoundObjectFactory _soundObjectFactory;
        [SerializeField] private Instrument instrument;

        protected override void Start()
        {
            UseInfo = "Change Instrument";
            _soundObjectFactory = transform.parent.parent.GetComponent<SoundObjectFactory>();
        }

        public override void Interact()
        {
            _soundObjectFactory.SetInstrument("Audio/Sounds/" + instrument);
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