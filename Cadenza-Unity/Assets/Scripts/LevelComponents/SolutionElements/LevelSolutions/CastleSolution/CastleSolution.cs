using SceneManagement;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.CastleSolution
{
    public class CastleSolution : LevelSolutionEvent
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject door;

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