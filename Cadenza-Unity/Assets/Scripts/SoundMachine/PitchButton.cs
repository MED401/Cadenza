using Event_System;
using Interactions;
using UnityEngine;

namespace SoundMachine
{
    public class PitchButton : Interactable, IButton
    {
        private SoundBox soundBox;
        public AudioClip Clip { get; set; }

        protected override void Start()
        {
            base.Start();
            
            soundBox = transform.parent.parent.GetComponent<SoundBox>();
        }

        protected override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;

            GameEvents.Current.ApplyPitch(soundBox.GetInstanceID(), Clip);
        }
    }
}