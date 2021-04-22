using System.Collections;
using Interactions;
using UnityEngine;

namespace LevelComponents.LevelSpecificComponents.Royal_Woods
{
    public class OpenGateButton : Interactable
    {
        [SerializeField] private GateController gateController;
        private bool _isRunning;

        public override void Interact()
        {
            if (!_isRunning) StartCoroutine(ActivateGate());
        }

        private IEnumerator ActivateGate()
        {
            _isRunning = true;
            gateController.OpenGate();

            yield return new WaitForSeconds(4);

            gateController.CloseGate();
            _isRunning = false;
        }
    }
}