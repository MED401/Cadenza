using System.Collections;
using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.LevelEvents
{
    public class CryptParticles : LevelEvent
    {
        public AudioSource pillarSound;
        [SerializeField] private Transform Road;
        private bool _pillarRisen;

        public override void Event(NoteScriptableObject note)
        {
            if (note != correctNoteForEvent) return;
            if (_pillarRisen) return;
            var PillarPosition = Road.position;
            StartCoroutine(LerpPosition(Road, PillarPosition += new Vector3(0, 35, 0), 6));

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
                    Vector3.Lerp(startPosition, targetLocation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}