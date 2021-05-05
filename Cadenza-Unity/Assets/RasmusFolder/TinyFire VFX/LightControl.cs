using UnityEngine;

namespace RasmusFolder.TinyFire_VFX
{
    public class LightControl : MonoBehaviour
    {
        private float _nRand;

        private void Update()
        {
            _nRand = Random.Range(4f, 5f);
            transform.GetComponent<Light>().intensity = _nRand;
        }
    }
}