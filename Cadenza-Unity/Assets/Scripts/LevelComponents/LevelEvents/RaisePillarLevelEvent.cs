using System.Collections;
using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.LevelEvents
{
    public class RaisePillarLevelEvent : LevelEvent
    {
        
        [SerializeField] private Transform _pillar;
        [SerializeField] private float Raise;
        public AudioSource pillarSound;

        private bool _pillarRisen;

        public override void Event(NoteScriptableObject note)
        {
            
            if (note != correctNoteForEvent) return;
            if (_pillarRisen) return;
            var PillarPosition = _pillar.position;
            StartCoroutine(LerpPosition(_pillar, PillarPosition += new Vector3(0,Raise,0), 3));

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