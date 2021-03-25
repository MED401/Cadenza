using Interactions;
using UnityEngine;

namespace SoundMachine
{
    public class SoundObject : MonoBehaviour
    {
        public AudioSource SoundSource { get; set; }
        private Pickup pickup;

        private void Start()
        {
            pickup = this.gameObject.AddComponent<Pickup>();
            SoundSource = GetComponent<AudioSource>();
        }
    }
}