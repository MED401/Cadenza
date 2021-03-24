using Event_System;
using UnityEngine;

namespace SoundMachine
{
    public class PitchButton : Button
    {
        [SerializeField] [Range(1, 5)] private int sound;
        [SerializeField]  private AudioClip clip; 


        protected override void Start()
        {
            base.Start();

            GameEvents.Current.OnChangeInstrument += OnChangeInstrument;
        }

        private void OnChangeInstrument(int id, string path)
        {
            if(transform.parent.parent.GetInstanceID() != id) return;

            Debug.Log(path + "/" + sound.ToString());;
            clip = Resources.Load<AudioClip>(path + "/" + sound.ToString()); 
        }

        public override void OnInteract(int id)
        {
        }
    }
}