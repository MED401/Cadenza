using System;
using LevelSystem;
using SoundMachine;
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

        public event Action<int> ONDrop;

        public void Drop(int id)
        {
            ONDrop?.Invoke(id);
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

        public event Action<int, SoundObject> ONValidatePlace;

        public void ValidatePlace(int id, SoundObject soundObject)
        {
            ONValidatePlace?.Invoke(id, soundObject);
        }

        public event Action<int> ONValidateSolution;

        public void ValidateSolution(int id)
        {
            ONValidateSolution?.Invoke(id);
        }
        
    }
}