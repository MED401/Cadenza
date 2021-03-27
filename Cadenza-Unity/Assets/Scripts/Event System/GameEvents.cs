using System;
using Interactions;
using LevelSystem;
using UnityEngine;

namespace Event_System
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents Current;

        private void Awake()
        {
            Current = this;
        }

        public event Action<int> ONTarget;

        public void TakeTarget(int id)
        {
            ONTarget?.Invoke(id);
        }

        public event Action<int> ONRemoveTarget;

        public void RemoveTarget(int id)
        {
            ONRemoveTarget?.Invoke(id);
        }

        public event Action<int> ONInteract;

        public void Interact(int id)
        {
            ONInteract?.Invoke(id);
        }

        public event Action<int, SoundObjectPlatform> ONPlace; 

        public void Place(int id, SoundObjectPlatform target)
        {
            ONPlace?.Invoke(id, target);
        }

        public event Action<int> ONPlateActivation;

        public void PlateActivation(int id)
        {
            ONPlateActivation?.Invoke(id);
        }

        public event Action<int> ONDrop;

        public void Drop(int id)
        {
            ONDrop?.Invoke(id);
        }
        
        public event Action<int> ONActivateDoor;

        public void ActivateDoor(int id)
        {
            ONActivateDoor?.Invoke(id);
        }

        public event Action<int, string> ONChangeInstrument;

        public void ChangeInstrument(int id, string path)
        {
            ONChangeInstrument?.Invoke(id, path);
        }

        public event Action<int, AudioClip> ONApplyPitch;

        public void ApplyPitch(int id, AudioClip clip)
        {
            ONApplyPitch?.Invoke(id, clip);
        }
    }
}