using System.Collections.Generic;
using System.Linq;
using Interactions;
using LevelComponents.SolutionElements;
using UnityEngine;

namespace LevelComponents
{
    public abstract class LevelSolutionEvent : MonoBehaviour
    {
        public abstract void OnLevelSolution();
    }

    public class LevelController : MonoBehaviour
    {
        public SoundObjectPlatform[] soundObjectPlatforms;
        public List<AudioClip> correctSoundClips;

        [SerializeField] private Transform exitDoor;
        private Portal _portal;

        private LevelSolutionEvent _solutionEvent;

        private void Start()
        {
            _portal = FindObjectOfType<Portal>();
            _portal.gameObject.SetActive(false);
            
            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>();
            _solutionEvent = GetComponent<LevelSolutionEvent>();

            foreach (var platform in soundObjectPlatforms)
                correctSoundClips.Add(platform.correctNote.clip);
        }

        public void ValidateSolution()
        {
            if (soundObjectPlatforms.Any(soundObjectPlatform => !soundObjectPlatform.Validate())) return;

            if (_solutionEvent)
            {
                _solutionEvent.OnLevelSolution();
            }
            else
            {
                _portal.OpenPortal();
                exitDoor.gameObject.SetActive(false);
            }
        }
    }
}