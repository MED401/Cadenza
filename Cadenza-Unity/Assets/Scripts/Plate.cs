using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    public int plateId;
    int objectsOnPlate = 0;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Pickup>() != null)        // OBS! Få hjælp af hector til at tilføje pickup på de boxe jeg har lavet OBS!
        {
            objectsOnPlate++;

            if(other.gameObject.GetComponent<Pickup>().ReturnBoxId() == padId)      //OBS! få et ID på boxen inde i pickup sådan så hvis plate og box ID matcher sidder boxen på den rigtige plate
            {
                //Increases the number of correct placements 
            }
        }

    }

    private void OnTriggerExit (Collider other)
    {
        if(other.gameObject.GetComponent<Pickup>() != null)
        {
            objectsOnPlate--;
            
            //Decrease the number of placements

        if(other.gameObject.GetComponent<Pickup>().ReturnBoxId() == plateId)        //OBS! husk at lav et box ID
            {
                //Decrease the number of correct placements 
            }
        }
    }
}
