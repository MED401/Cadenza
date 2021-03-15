using Event_System;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        protected int ID;
        protected Outline Outline;

        private void Awake()
        {
            Outline = gameObject.AddComponent<Outline>();
            Outline.OutlineColor = Color.white;
            Outline.OutlineWidth = 3;
            Outline.OutlineMode = Outline.Mode.OutlineVisible;
            Outline.enabled = false;
            ID = GetInstanceID();
        }

        protected virtual void Start()
        {
            GameEvents.Current.ONRemoveTarget += OnRemoveTarget;
            GameEvents.Current.ONTarget += OnTarget;
        }

        public abstract void OnInteract(int id);

        public int GetId()
        {
            return ID;
        }

        public void OnTarget(int id)
        {
            if (ID != id) return;

            Outline.enabled = true;
        }

        public void OnRemoveTarget(int id)
        {
            if (ID != id) return;

            Outline.enabled = false;
        }
    }
}