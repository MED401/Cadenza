using Event_System;
using UnityEngine;

namespace Interactions
{
    public class SwapMaterial : Targetable, IInteractable
    {
        [SerializeField] private Material onInteractMaterial;
        private Material baseMaterial;
        private new Renderer renderer;

        protected override void Start()
        {
            base.Start();
            renderer = GetComponent<Renderer>();
            var material = renderer.material;
            baseMaterial = material;

            GameEvents.Current.ONInteract += OnInteract;
        }

        public void OnInteract(int id)
        {
            if (this.ID != id) return;

            if (renderer.material == baseMaterial)
                renderer.material = onInteractMaterial;
            else
                renderer.material = baseMaterial;
        }
    }
}