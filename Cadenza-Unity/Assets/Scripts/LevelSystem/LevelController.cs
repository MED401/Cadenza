using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Event_System;
using SoundMachine;
using UnityEngine;

namespace LevelSystem
{
    public class LevelController : MonoBehaviour
    {
        public AudioSource doorSound;
        public AudioSource pillarSound;
        public SolutionLight[] solutionLights;
        
        [SerializeField] private SoundObjectPlatform[] soundObjectPlatforms;
        [SerializeField] private Transform exitDoor;
        [SerializeField] private Transform moveTransformTarget;
        
        private bool pillarRisen = false;
        

        
        public AudioClip[] CorrectSoundClips { get; set; }

        private void Start()
        {
            GameEvents.Current.ONValidateSolution += OnValidateSolution;

            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>();
            CorrectSoundClips = new AudioClip[soundObjectPlatforms.Length];

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

            exitDoor.position = Vector3.zero;
            doorSound.Play();
        }

        public void EMoveTransform()
        {
            if (!pillarRisen)
            {
                StartCoroutine(LerpPosition(moveTransformTarget, new Vector3(0, 5, 0), 3f));
                pillarSound.Play();
                pillarRisen = true;
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