using System.Collections;
using UnityEngine;

namespace PassiveInteractions
{
    public class PressurePlateDoor : MonoBehaviour
    {
        [SerializeField] private Transform gate;
        [SerializeField] private AudioSource gateSound;
        private bool _isOpened;

        private void OnTriggerEnter(Collider id)
        {
            if (_isOpened) return;
            var gatePosition = gate.position;
            StartCoroutine(LerpPosition(gate, gatePosition += new Vector3(0, (float) -3.4, 0), (float) 4.5));

            _isOpened = true;
            gateSound.Play();
        }

        private IEnumerator LerpPosition(Transform targetObject, Vector3 targetLocation, float duration)
        {
            float time = 0;
            var startPosition = targetObject.position;

            while (time < duration)
            {
                targetObject.position =
                    Vector3.Lerp(startPosition, targetLocation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}