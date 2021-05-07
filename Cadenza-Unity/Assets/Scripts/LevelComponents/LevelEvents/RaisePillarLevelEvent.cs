using System.Collections;
using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.LevelEvents
{
    public class RaisePillarLevelEvent : LevelEvent
    {
        public AudioSource pillarSound;

        [SerializeField] private Transform pillar;
        [SerializeField] private float raiseSpeed;
        [SerializeField] private float raiseDistance;

        private bool _pillarRisen;
        private SoundObjectPlatform _soundObjectPlatform;

        private void Start()
        {
            _soundObjectPlatform = GetComponent<SoundObjectPlatform>();
        }

        public override void Event(NoteScriptableObject note)
        {
            if (note != correctNoteForEvent) return;
            if (_pillarRisen) return;

            StartCoroutine(LerpPosition(pillar.position + new Vector3(0, raiseDistance, 0)));

            pillarSound.Play();
            _pillarRisen = true;
            _soundObjectPlatform.EnableAltLight();
        }

        private IEnumerator LerpPosition(Vector3 targetLocation)
        {
            while (pillar.position != targetLocation)
            {
                pillar.position = Vector3.MoveTowards(pillar.position, targetLocation, raiseSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}