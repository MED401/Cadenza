using SceneManagement;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.CastleSolution
{
    public class CastleSolution : LevelSolutionEvent
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject door;
        [SerializeField] private Portal portal;

        public override void OnLevelSolution()
        {
            audioSource.Play();
            portal.OpenPortal();
            door.SetActive(false);
        }

        public override void OnNoLevelSolution()
        {
            audioSource.Play();
            portal.ClosePortal();
            door.SetActive(true);
        }
    }
}