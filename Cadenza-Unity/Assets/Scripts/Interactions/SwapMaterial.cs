using UnityEngine;

namespace Interactions
{
    public class SwapMaterial : Interactable
    {
        [SerializeField] private Material onTargetMaterial;
        [SerializeField] private Material onInteractMaterial;

        [SerializeField] private int _id;
        private new Renderer renderer;
        private Material baseMaterial;

        private Material currentMaterial;

        public override int id => _id;

        private void Start()
        {
            renderer = GetComponent<Renderer>();
            var material = renderer.material;
            baseMaterial = material;
            currentMaterial = material;

            GameEvents.Current.ONInteract += OnInteract;
            GameEvents.Current.ONTarget += OnTarget;
            GameEvents.Current.ONRemoveTarget += OnRemoveTarget;
        }

        public override void OnInteract(int id)
        {
            if (id != this.id) return;
            currentMaterial = currentMaterial != onInteractMaterial ? onInteractMaterial : baseMaterial;
            renderer.material = currentMaterial;
        }

        public override void OnTarget(int id)
        {
        }
        public override void OnRemoveTarget(int id)
        {
            if (id != this.id) return;
            renderer.material = currentMaterial;
        }
    }
}
