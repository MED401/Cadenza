using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.GardenSolutions
{
    public class GardenLevelSolution : LevelSolutionEvent
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject door;

        public override void OnLevelSolution()
        {
            Portal.OpenPortal();
            audioSource.Play();
            door.SetActive(false);
        }

        public override void OnNoLevelSolution()
        {
            Portal.ClosePortal();
            audioSource.Play();
            door.SetActive(true);
        }
    }
}