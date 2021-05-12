using System.Collections;
using UnityEngine;

namespace LevelComponents.LevelSpecificComponents.Royal_Woods
{
    public class GateController : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private AudioClip gateAudioClip;
        private AudioSource _gateAudioSource;
        private Vector3 _closePosition;
        private IEnumerator _moveRoutine;
        private Vector3 _openPosition;

        private void Awake()
        {
            _gateAudioSource = gameObject.AddComponent<AudioSource>();
            _gateAudioSource.clip = gateAudioClip;
            _gateAudioSource.spatialBlend = 0.8f;

            var position = transform.position;
            _closePosition = position;
            _openPosition = position + new Vector3(0, 3, 0);
        }

        public void OpenGate()
        {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);

            _moveRoutine = MoveGate(_openPosition);
            StartCoroutine(_moveRoutine);
        }

        public void CloseGate()
        {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);

            _moveRoutine = MoveGate(_closePosition);
            StartCoroutine(_moveRoutine);
        }

        private IEnumerator MoveGate(Vector3 targetPosition)
        {
            _gateAudioSource.Play();
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}