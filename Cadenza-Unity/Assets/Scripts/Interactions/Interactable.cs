using Event_System;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        protected Outline Outline;

        private void Awake()
        {
            Outline = gameObject.AddComponent<Outline>();
            Outline.OutlineColor = Color.white;
            Outline.OutlineWidth = 3;
            Outline.OutlineMode = Outline.Mode.OutlineVisible;
            Outline.enabled = false;
        }

        protected virtual void Start()
        {
            GameEvents.Current.ONInteract += OnInteract;
            GameEvents.Current.ONRemoveTarget += OnRemoveTarget;
            GameEvents.Current.ONTarget += OnTarget;
        }

        public abstract void OnInteract(int id);


        public void OnTarget(int id)
        {
            if (this.GetInstanceID() != id) return;

            Outline.enabled = true;
        }

        public void OnRemoveTarget(int id)
        {
            if (this.GetInstanceID() != id) return;

            Outline.enabled = false;
        }
    }
}