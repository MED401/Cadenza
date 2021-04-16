using UnityEngine;

namespace LevelComponents.DisplayElements
{
    public class ConeLight : SolutionLight
    {
        [SerializeField] private GameObject cone;

        public override void TurnOn()
        {
            base.TurnOn();
            cone.SetActive(true);
        }

        public override void TurnOff()
        {
            base.TurnOff();
            cone.SetActive(false);
        }
    }
}