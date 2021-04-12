using UnityEngine;

namespace LevelComponents.DisplayElements
{
    public class SolutionLight : MonoBehaviour
    {
        [SerializeField] private Material onMaterial;
        [SerializeField] private Material offMaterial;
        private new Light _light;

        private void Awake()
        {
            offMaterial = GetComponent<MeshRenderer>().material;
            _light = gameObject.AddComponent<Light>();
            _light.enabled = false;
            _light.color = offMaterial.color; 
        }

        public virtual void TurnOn()
        {
            _light.enabled = true;
            GetComponent<MeshRenderer>().material = onMaterial;
        }

        public virtual void TurnOff()
        {
            _light.enabled = false;
            GetComponent<MeshRenderer>().material = offMaterial;
        }
    }
}
