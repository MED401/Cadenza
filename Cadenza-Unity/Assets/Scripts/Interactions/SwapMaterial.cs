using UnityEngine;

namespace Interactions
{
    public class SwapMaterial : Interactable
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
        }

        protected override void OnInteract(int id)
        {
            if (this.GetInstanceID() != id) return;

            if (renderer.material == baseMaterial)
                renderer.material = onInteractMaterial;
            else
                renderer.material = baseMaterial;
        }
    }
}