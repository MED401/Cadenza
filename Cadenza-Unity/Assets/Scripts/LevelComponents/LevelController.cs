using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private Transform portalActive;

        private LevelSolutionEvent _solutionEvent;

        private void Start()
        {
            if (portalActive) portalActive.gameObject.SetActive(false);

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
                portalActive.gameObject.SetActive(true);
                exitDoor.gameObject.SetActive(false);
            }
        }
    }
}