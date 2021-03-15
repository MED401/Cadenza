using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlateManager : MonoBehaviour
{

    public static PlateManager instance;

    public List<GameObject> plate;
    public List<GameObject> box;
    public List<Color> color; //OBS! this have to be sound instead!

    public int totalCorrectPlacements;
    public int currentCorrectPlacements;
    public int placements;

    public Text canvasText;
    public UnityEvent completeEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        totalCorrectPlacements = plate.Count;
        currentCorrectPlacements = 0;
        AssignColor(box);
        AssignColor(plate);
        BoxOrder();

    }

    public void IncreasePlacement()
    {
        placements++;

        if(placements == totalCorrectPlacements)    //update the canvas text
        {
            canvasText.text = currentCorrectPlacements.ToString();
        }
    }

    public void DecreasePlacement()
    {
        placements--;
    }

    public IncreaseCorrectPlacement()
    {
        currentCorrectPlacements++;

        if(currentCorrectPlacements == totalCorrectPlacements)
        {
            Debug.Log("Everything is placed correctly");
            completeEvent.Invoke();
        }
    }

    public void DecreaseCorrectPlacement()
    {
        currentCorrectPlacements--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignColor(List<GameObject> objects)
    {
        for(int i = 0; i < objects.Count; i++)
        {
            objects[i].GetComponent<Renderer>().material.color = color[i]; 
        }
    }

    void BoxOrder()
    {
        int number = 0; //Box ID
        for(int i = 0; i < box.Count; i++)
        {
            GameObject temp = box[i];       //box stored in temp variable
            int randomIndex = UnityEngine.Random.Range(i, box.Count);
            box[i] = box[randomIndex]; 
            box[randomIndex] = temp;

            box[i].GetComponent<Pickup>().boxId = number;
            plate[i].GetComponent<Plate>().plateId = number;
            number++;
        }
    }
}
