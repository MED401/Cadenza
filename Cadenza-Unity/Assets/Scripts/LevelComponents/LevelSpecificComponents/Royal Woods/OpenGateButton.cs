using System.Collections;
using Interactions;
using UnityEngine;

namespace LevelComponents.LevelSpecificComponents.Royal_Woods
{
    public class OpenGateButton : Interactable
    {
        private GateController _gateController;
        private bool _isRunning;

        protected override void Start()
        {
            base.Start();
            _gateController = transform.parent.GetComponentInChildren<GateController>();
        }

        public override void Interact()
        {
            if (!_isRunning) StartCoroutine(ActivateGate());
        }

        private IEnumerator ActivateGate()
        {
            _isRunning = true;
            _gateController.OpenGate();

            yield return new WaitForSeconds(4);

            _gateController.CloseGate();
            _isRunning = false;
        }
    }
}