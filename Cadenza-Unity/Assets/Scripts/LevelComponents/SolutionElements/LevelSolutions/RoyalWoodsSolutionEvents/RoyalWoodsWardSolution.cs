using LevelComponents.LevelSpecificComponents.Royal_Woods;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.RoyalWoodsSolutionEvents
{
    public class RoyalWoodsWardSolution : LevelSolutionEvent
    {
        [SerializeField] private GateController gateController;
        [SerializeField] private GameObject doorButton;
        
        public override void OnLevelSolution()
        {
            gateController.OpenGate();
            Destroy(doorButton.GetComponent<OpenGateButton>());
        }
    }
}