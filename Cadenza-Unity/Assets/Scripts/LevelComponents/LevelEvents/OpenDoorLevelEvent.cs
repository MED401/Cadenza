using System.Collections;
using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.LevelEvents
{
    public class OpenDoorLevelEvent : LevelEvent
    {
        public AudioSource doorSound;

        [SerializeField] private Transform door;
        
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

            StartCoroutine(LerpPosition(door, new Vector3(0, -3, 0), 1));

            doorSound.Play();
            _soundObjectPlatform.EnableAltLight();
            _doorRisen = true;
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