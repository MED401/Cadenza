using Event_System;
using UnityEngine;

namespace SoundMachine
{
    public class PitchButton : Button
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