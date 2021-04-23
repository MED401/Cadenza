using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.RoyalWoodsSolutionEvents
{
    public class RoyalWoodsMainSolution : LevelSolutionEvent
    {
        [SerializeField] private GameObject portal;

        public override void OnLevelSolution()
        {
            portal.SetActive(true);
        }
    }
}