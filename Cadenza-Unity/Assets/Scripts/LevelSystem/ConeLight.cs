using UnityEngine;

namespace LevelSystem
{
    public class ConeLight : SolutionLight
    {
        [SerializeField] private GameObject Cone;
        public override void TurnOn()
        {
            
            base.TurnOn();
            Cone.SetActive(true);
            
        }

        public override void TurnOff()
        {
            base.TurnOff();
            Cone.SetActive(false);
        }
    }
}