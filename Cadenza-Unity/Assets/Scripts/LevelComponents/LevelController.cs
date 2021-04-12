using System.Collections;
using Event_System;
using LevelComponents.DisplayElements;
using LevelComponents.SolutionElements;
using UnityEngine;

namespace LevelComponents
{
    public class LevelController : MonoBehaviour
    {
        public AudioSource doorSound;
        public AudioSource pillarSound;
        public SolutionLight[] solutionLights;
        
        [SerializeField] private SoundObjectPlatform[] soundObjectPlatforms;
        [SerializeField] private Transform exitDoor;
        [SerializeField] private Transform moveTransformTarget;
        
        private bool _pillarRisen = false;
        
        public AudioClip[] CorrectSoundClips { get; set; }

        private void Start()
        {
            GameEvents.Current.ONValidateSolution += OnValidateSolution;

            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>();
            CorrectSoundClips = new AudioClip[soundObjectPlatforms.Length];

            doorSound.spatialBlend = 0.8f;

            solutionLights = GetComponentsInChildren<SolutionLight>();
            for (var i = 0; i < soundObjectPlatforms.Length; i++)
                CorrectSoundClips[i] = soundObjectPlatforms[i].GetCorrectAudioClip();
        }

        private void OnValidateSolution(int id)
        {
            if (GetInstanceID() != id) return;

            foreach (SoundObjectPlatform soundObjectPlatform in soundObjectPlatforms)
            {
                if (!soundObjectPlatform.HasCorrectAudioClip) return;
            }

            exitDoor.gameObject.SetActive(false);
            doorSound.Play();
        }

        public void EMoveTransform()
        {
            if (!_pillarRisen)
            {
                StartCoroutine(LerpPosition(moveTransformTarget, new Vector3(0, 5, 0), 3f));
                pillarSound.Play();
                _pillarRisen = true;
            }
        }
            

        private IEnumerator LerpPosition(Transform targetObject, Vector3 targetLocation, float duration)
        {
            float time = 0;
            var startPosition = targetObject.position;

            while (time < duration)
            {
                targetObject.position = Vector3.Lerp(startPosition, startPosition + targetLocation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}