﻿using System.Collections;
using UnityEngine;

namespace LevelComponents.LevelSpecificComponents.Royal_Woods
{
    public class GateController : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private Vector3 _closePosition;
        private IEnumerator _moveRoutine;
        private Vector3 _openPosition;
        public AudioSource gateSound;

        [SerializeField] private AudioClip gateClip;

        private void Awake()
        {
            gateSound = gameObject.AddComponent<AudioSource>();
            gateSound.clip = gateClip;
            var position = transform.position;
            _closePosition = position;
            _openPosition = position + new Vector3(0, 3, 0);
        }

        public void OpenGate()
        {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);

            gateSound.Play();
            _moveRoutine = MoveGate(_openPosition);
            StartCoroutine(_moveRoutine);
        }

        public void CloseGate()
        {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);
            
            gateSound.Play();    
            _moveRoutine = MoveGate(_closePosition);
            StartCoroutine(_moveRoutine);
        }

        private IEnumerator MoveGate(Vector3 targetPosition)
        {
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}