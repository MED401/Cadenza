using Interactions;
using UnityEngine;

namespace LevelSystem
{
    public class PlayCurrentSoundButton : Interactable, IButton
    {
        public AudioClip[] correctSoundClips; 

        protected override void OnInteract(int id)
        {
            
        }
    }
}