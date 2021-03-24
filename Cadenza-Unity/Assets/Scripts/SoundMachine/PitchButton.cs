using UnityEngine;

namespace SoundMachine
{
    public class PitchButton : Button
    {
        public AudioClip clip { get; set; }


        protected override void Start()
        {
            base.Start();
        }

        public override void OnInteract(int id)
        {
        }
    }
}