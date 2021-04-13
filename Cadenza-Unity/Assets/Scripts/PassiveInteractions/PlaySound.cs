using UnityEngine;

namespace PassiveInteractions
{
    public class PlaySound : MonoBehaviour
    {
        public AudioSource audioClip;
        public bool alreadyPlayed;

        public AudioSource door;

        private void Start()
        {
            audioClip = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter()
        {
            if (alreadyPlayed) return;

            audioClip.Play();
            door.Play();
            alreadyPlayed = true;
        }
    }
}