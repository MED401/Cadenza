using Event_System;
using UnityEngine;

namespace SoundMachine
{
    public class InstrumentButton : Button
    {
        [SerializeField] private string instrumentPath;
        private SoundBox soundBox; 

        protected override void Start()
        {
            base.Start();

            soundBox = transform.parent.parent.GetComponent<SoundBox>();
        }

        public override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;

            GameEvents.Current.ChangeInstrument(transform.parent.parent
                .GetInstanceID(), instrumentPath);
        }
    }
}