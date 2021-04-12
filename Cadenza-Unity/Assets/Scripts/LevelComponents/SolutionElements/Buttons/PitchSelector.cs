using Event_System;
using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements.Buttons
{
    public class PitchSelector : Interactable
    {
        private SoundObjectFactory _soundObjectFactory;
        public AudioClip Clip { get; set; }

        protected override void Start()
        {
            UseInfo = "Change Pitch"; 
            _soundObjectFactory = transform.parent.parent.GetComponent<SoundObjectFactory>();
        }

        public override void Interact()
        {
            _soundObjectFactory.SetPitch(clip: Clip);
        }
    }
}