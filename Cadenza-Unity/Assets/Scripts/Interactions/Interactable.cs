using Event_System;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        protected Outline outline;

        protected virtual void Awake()
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 3;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.enabled = false;
        }

        protected virtual void Start()
        {
            GameEvents.Current.ONInteract += OnInteract;
            GameEvents.Current.ONRemoveTarget += OnRemoveTarget;
            GameEvents.Current.ONTarget += OnTarget;
        }

        protected abstract void OnInteract(int id);


        protected virtual void OnTarget(int id)
        {
            if (GetInstanceID() != id) return;

            outline.enabled = true;
        }

        protected virtual void OnRemoveTarget(int id)
        {
            if (GetInstanceID() != id) return;

            outline.enabled = false;
        }
    }
}