using Event_System;
using Interactions;
using UnityEngine;

namespace LevelComponents.SolutionElements.Buttons
{
    public class PitchButton : Interactable
    {
        private SoundObjectFactory soundObjectFactory;
        public AudioClip Clip { get; set; }

        protected override void Start()
        {
            UseInfo = "Change Pitch"; 
            soundObjectFactory = transform.parent.parent.GetComponent<SoundObjectFactory>();
        }

        public override void Interact()
        {
            GameEvents.Current.ApplyPitch(soundObjectFactory.GetInstanceID(), Clip);
        }
    }
}