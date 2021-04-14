using Event_System;
using Interactions;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements.Buttons
{
    public class InstrumentSelector : Interactable
    {
        public InstrumentScriptableObject instrument;
        private SoundObjectFactory _soundObjectFactory;

        protected override void Start()
        {
            UseInfo = "Change Instrument";
            _soundObjectFactory = transform.parent.parent.GetComponent<SoundObjectFactory>();
        }

        public override void Interact()
        {
            _soundObjectFactory.SetInstrument(instrument);
        }
      

   
    }
}