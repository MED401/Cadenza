using System;
using UnityEngine;

namespace LevelSystem
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

        public void TurnOn()
        {
            light.enabled = true;
            GetComponent<MeshRenderer>().material = onMaterial;
        }

        public void TurnOff()
        {
            light.enabled = false;
            GetComponent<MeshRenderer>().material = offMaterial;
        }
    }
}
