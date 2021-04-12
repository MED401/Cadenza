using UnityEngine;

namespace LevelComponents.DisplayElements
{
    public class SolutionLight : MonoBehaviour
    {
        [SerializeField] private Material onMaterial;
        [SerializeField] private Material offMaterial;
        private new Light light;

        private void Awake()
        {
            offMaterial = GetComponent<MeshRenderer>().material;
            light = gameObject.AddComponent<Light>();
            light.enabled = false;
            light.color = offMaterial.color; 
        }

        public virtual void TurnOn()
        {
            light.enabled = true;
            GetComponent<MeshRenderer>().material = onMaterial;
        }

        public virtual void TurnOff()
        {
            light.enabled = false;
            GetComponent<MeshRenderer>().material = offMaterial;
        }
    }
}
