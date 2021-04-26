using LevelComponents.LevelSpecificComponents.Royal_Woods;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.BlackTowerSolutionEvents
{
    public class BlackTowerWardSolution : LevelSolutionEvent
    {
        [SerializeField] private GateController gateController;
        
        public override void OnLevelSolution()
        {
            gateController.OpenGate();
        }
    }
}
