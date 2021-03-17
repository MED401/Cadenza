using System.Collections;
using Event_System;
using UnityEngine;

namespace Interactions
{
    public class Plate : Interactable
    {
        public Transform placementLocation;
        private ArrayList correctObjects;
        private Pickup objectOnPlate;
        private int objectsOnPlate;

        protected override void Start()
        {
            base.Start();

            GameEvents.Current.OnPlateActivation += OnPlateActivation;
        }

        private void OnPlateActivation(int id)
        {
            if (GetInstanceID() != id) return;
        }

        public override void OnInteract(int id)
        {
            if (GetInstanceID() != id) return;
        }
    }
}