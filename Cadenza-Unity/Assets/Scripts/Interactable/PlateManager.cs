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
        AssignColor(box);
        AssignColor(plate);

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
}
