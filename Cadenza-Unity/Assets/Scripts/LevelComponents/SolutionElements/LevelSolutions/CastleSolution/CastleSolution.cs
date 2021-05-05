using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.CastleSolution
{
    public class CastleSolution : LevelSolutionEvent
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject door;
        [SerializeField] private Transform portal;
        private LevelSolutionEvent _levelSolutionEventImplementation;

        public override void OnLevelSolution()
        {
            audioSource.Play();
            Portal.OpenPortal();
            door.SetActive(false);
        }

        public override void OnNoLevelSolution()
        {
            audioSource.Play();
            Portal.ClosePortal();
            door.SetActive(true);
        }
    }
}