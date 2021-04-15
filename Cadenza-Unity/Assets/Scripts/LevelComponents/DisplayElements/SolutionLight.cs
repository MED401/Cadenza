using UnityEngine;

namespace LevelComponents.DisplayElements
{
    public class SolutionLight : MonoBehaviour
    {
        [SerializeField] private Material onMaterial;
        [SerializeField] private Material offMaterial;
        private Light _light;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            offMaterial = _meshRenderer.material;
            _light = gameObject.AddComponent<Light>();
            _light.enabled = false;
            _light.color = offMaterial.color;
        }

        public virtual void TurnOn()
        {
            _light.enabled = true;
            _meshRenderer.material = onMaterial;
        }

        public virtual void TurnOff()
        {
            _light.enabled = false;
            _meshRenderer.material = offMaterial;
        }
    }
}