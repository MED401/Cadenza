using System;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        public string UseInfo { get; protected set; }
        protected Outline Outline;

        protected virtual void Awake()
        {
            Outline = gameObject.AddComponent<Outline>();
            Outline.OutlineColor = Color.white;
            Outline.OutlineWidth = 3;
            Outline.OutlineMode = Outline.Mode.OutlineVisible;
            Outline.enabled = false;
        }

        protected virtual void Start()
        {
            UseInfo = "Interact";
        }

        public abstract void Interact();


        public virtual void Target()
        {
            Outline.enabled = true;
        }

        public virtual void RemoveTarget()
        {
            Outline.enabled = false;
        }
    }
}