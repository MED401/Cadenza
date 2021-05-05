using LevelComponents.LevelSpecificComponents.Royal_Woods;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.RoyalWoodsSolutionEvents
{
    public class RoyalWoodsWardSolution : LevelSolutionEvent
    {
        [SerializeField] private GateController gateController;
        [SerializeField] private GameObject doorButton;
        private OpenGateButton _openGateButton;

        protected new void Start()
        {
            base.Start();
            _openGateButton = doorButton.AddComponent<OpenGateButton>();
        }

        public override void OnLevelSolution()
        {
            gateController.OpenGate();
            Destroy(_openGateButton);
        }

        public override void OnNoLevelSolution()
        {
            gateController.CloseGate();
            if (!_openGateButton) _openGateButton = doorButton.AddComponent<OpenGateButton>();
        }
    }
}