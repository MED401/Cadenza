using System;
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

        public event Action<int> ONValidateSolution;

        public void ValidateSolution(int id)
        {
            ONValidateSolution?.Invoke(id);
        }
        
    }
}