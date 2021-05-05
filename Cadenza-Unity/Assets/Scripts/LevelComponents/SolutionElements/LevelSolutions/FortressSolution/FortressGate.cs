using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.FortressSolution
{
    public class FortressGate : LevelEvent
    {
        
        [SerializeField] private Transform _gate;
        public AudioSource gateSound;

        private bool _gateOpen;

        public override void Event(NoteScriptableObject note)
        {
            
            if (note != correctNoteForEvent) return;
            if (_gateOpen) return;
            var gatePosition = _gate.position;
            StartCoroutine(LerpPosition(_gate, gatePosition += new Vector3(0,(float)0.4,0), (float)1.5));

            gateSound.Play();
            _gateOpen = true;
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