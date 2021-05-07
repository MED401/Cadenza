using System;
using System.Collections;
using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.LevelEvents
{
    public class CloseDoorLevelEvent : LevelEvent
    {
        
        [SerializeField] private Transform door;
        public AudioSource doorSound;

        private bool _doorRisen;
        private SoundObjectPlatform _soundObjectPlatform;

        private void Start()
        {
            _soundObjectPlatform = GetComponent<SoundObjectPlatform>();
        }

        public override void Event(NoteScriptableObject note)
        {
            if (note != correctNoteForEvent) return;
            if (_doorRisen) return;

            var doorPosition = door.position;
            StartCoroutine(LerpPosition(door, doorPosition += new Vector3(0,3,0), 1));

            doorSound.Play();
            _doorRisen = true;
            _soundObjectPlatform.EnableAltLight();
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