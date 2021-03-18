using UnityEngine;

namespace Interactions
{


    public class Plate : MonoBehaviour
    {

        public int plateId;
        int objectsOnPlate = 0;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Interactable>() != null)        // OBS! F� hj�lp af hector til at tilf�je pickup p� de boxe jeg har lavet OBS!
            {
                objectsOnPlate++;

                //if(other.gameObject.GetComponent<Pickup>().ReturnBoxId() == padId)      //OBS! f� et ID p� boxen inde i pickup s�dan s� hvis plate og box ID matcher sidder boxen p� den rigtige plate
                {
                    //Increases the number of correct placements 
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Interactable>() != null)
            {
                objectsOnPlate--;

                //Decrease the number of placements

                //if(other.gameObject.GetComponent<Pickup>().ReturnBoxId() == plateId)        //OBS! husk at lav et box ID
                {
                    //Decrease the number of correct placements 
                }
            }
        }
    }
}