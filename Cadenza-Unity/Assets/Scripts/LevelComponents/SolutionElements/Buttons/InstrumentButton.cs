﻿using Event_System;
using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements.Buttons
{
    public class InstrumentButton : Interactable
    {
        [SerializeField] private Instrument instrument;
        private SoundObjectFactory _soundObjectFactory;

        protected override void Start()
        {
            UseInfo = "Change Instrument";
            _soundObjectFactory = transform.parent.parent.GetComponent<SoundObjectFactory>();
        }

        public override void Interact()
        {
            GameEvents.Current.ChangeInstrument(_soundObjectFactory.GetInstanceID(), "Audio/Sounds/" + instrument);
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