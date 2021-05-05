using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        private Outline _outline;
        public string UseInfo { get; protected set; }

        protected virtual void Awake()
        {
            _outline = gameObject.AddComponent<Outline>();
            _outline.OutlineColor = Color.white;
            _outline.OutlineWidth = 3;
            _outline.OutlineMode = Outline.Mode.OutlineVisible;
            _outline.enabled = false;
        }

        protected virtual void Start()
        {
            UseInfo = "Interact";
        }

        public abstract void Interact();


        public virtual void Target()
        {
            _outline.enabled = true;
        }

        public virtual void RemoveTarget()
        {
            _outline.enabled = false;
        }
    }
}