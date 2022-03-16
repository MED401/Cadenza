using UnityEngine;

namespace EnvironmentEffects
{
    public class CogRotationDriver : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            RotateCog();
        }

        private void RotateCog()
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}