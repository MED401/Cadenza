using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.CastleSolution
{
    public class CastleSolution : LevelSolutionEvent
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject door;
        [SerializeField] private Transform portal;

        public override void OnLevelSolution()
        {
            portal.gameObject.SetActive(true);
            door.SetActive(false);
            _audioSource.Play();
        }
    }
}