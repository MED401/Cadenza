﻿using System.Collections;
using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.LevelEvents
{
    public class RaisePillarLevelEvent : LevelEvent
    {
        [SerializeField] private NoteScriptableObject _correctNote;
        [SerializeField] private Transform _pillar;
        public AudioSource pillarSound;

        private bool _pillarRisen;

        public override void Event(NoteScriptableObject note)
        {
            if (note != _correctNote) return;
            if (_pillarRisen) return;

            pillarSound.Play();
            _pillarRisen = true;
        }

        private IEnumerator LerpPosition(Transform targetObject, Vector3 targetLocation, float duration)
        {
            float time = 0;
            var startPosition = targetObject.position;

            while (time < duration)
            {
                targetObject.position =
                    Vector3.Lerp(startPosition, startPosition + targetLocation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}