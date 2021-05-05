using UnityEngine;

namespace Scenes._2nd_Iteration_Scenes.Lava.Rocks_and_Boulders_2.Shaders
{
    public class RotateGameObject : MonoBehaviour
    {
        public float rotSpeedX;
        public float rotSpeedY;
        public float rotSpeedZ;
        public bool local;


        private void FixedUpdate()
        {
            if (local)
                transform.Rotate(transform.up, Time.fixedDeltaTime * rotSpeedX);
            else
                transform.Rotate(Time.fixedDeltaTime * new Vector3(rotSpeedX, rotSpeedY, rotSpeedZ), Space.World);
        }
    }
}