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

        public event Action<int> ONValidateSolution;

        public void ValidateSolution(int id)
        {
            ONValidateSolution?.Invoke(id);
        }
    }
}