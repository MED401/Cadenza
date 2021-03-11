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

        public event Action<int> OnDrop;

        public void Drop(int id)
        {
            OnDrop?.Invoke(id);
        }
    }
}