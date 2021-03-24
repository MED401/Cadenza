using Event_System;
using UnityEngine;

namespace SoundMachine
{
    public class InstrumentButton : Button
    {
        [SerializeField] private string instrumentPath;
        private Button[] pitchButtons; 

        protected override void Start()
        {
            base.Start();

            pitchButtons = transform.parent.parent.GetChild(1).GetComponentsInChildren<Button>();
        }

        public override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;

            GameEvents.Current.ChangeInstrument(transform.parent.parent
                .GetInstanceID(), instrumentPath);
        }
    }
}