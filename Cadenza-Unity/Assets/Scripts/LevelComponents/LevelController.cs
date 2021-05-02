using System.Collections.Generic;
using System.Linq;
using LevelComponents.SolutionElements;
using SceneManagement;
using UnityEngine;

namespace LevelComponents
{
    public abstract class LevelSolutionEvent : MonoBehaviour
    {
        protected Portal Portal;

        protected void Start()
        {
            Portal = FindObjectOfType<Portal>();
        }

        public abstract void OnLevelSolution();
        public abstract void OnNoLevelSolution();
    }

    public class LevelController : MonoBehaviour
    {
        public SoundObjectPlatform[] soundObjectPlatforms;
        public List<AudioClip> correctSoundClips;

        [SerializeField] private Transform exitDoor;
        private bool _hasSolutionEvent;
        private Portal _portal;
        private LevelSolutionEvent _solutionEvent;
        private bool _solutionEventActivated;

        private void Start()
        {
            _portal = FindObjectOfType<Portal>();
            _portal.ClosePortal();

            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>();
            _solutionEvent = GetComponent<LevelSolutionEvent>();

            foreach (var platform in soundObjectPlatforms)
                correctSoundClips.Add(platform.correctNote.clip);

            _hasSolutionEvent = _solutionEvent != null;
        }

        private void Update()
        {
            ValidateSolution();
        }

        private void ValidateSolution()
        {
            if (_hasSolutionEvent)
            {
                if (soundObjectPlatforms.Any(soundObjectPlatform => !soundObjectPlatform.HasValidSolution) &&
                    _solutionEventActivated)
                {
                    _solutionEvent.OnNoLevelSolution();
                    _solutionEventActivated = false;
                }
                else if (soundObjectPlatforms.All(soundObjectPlatform => soundObjectPlatform.HasValidSolution) &&
                         !_solutionEventActivated)
                {
                    _solutionEvent.OnLevelSolution();
                    _solutionEventActivated = true;
                }
            }
            else
            {
                if (soundObjectPlatforms.Any(soundObjectPlatform => !soundObjectPlatform.HasValidSolution) &&
                    _solutionEventActivated)
                {
                    _portal.ClosePortal();
                }
                else if (soundObjectPlatforms.All(soundObjectPlatform => soundObjectPlatform.HasValidSolution) &&
                         !_solutionEventActivated)
                {
                    _portal.OpenPortal();
                    exitDoor.gameObject.SetActive(false);
                }
            }
        }
    }
}