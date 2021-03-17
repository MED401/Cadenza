using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Interactions
{
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
                instance = this;
            else if (instance != this) Destroy(gameObject);
        }

        // Start is called before the first frame update
        private void Start()
        {
            AssignColor(box);
            AssignColor(plate);
            BoxOrder();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void AssignColor(List<GameObject> objects)
        {
            for (var i = 0; i < objects.Count; i++) objects[i].GetComponent<Renderer>().material.color = color[i];
        }

        private void BoxOrder()
        {
            var number = 0; //Box ID
            for (var i = 0; i < box.Count; i++)
            {
                var temp = box[i]; //box stored in temp variable
                var randomIndex = Random.Range(i, box.Count);
                box[i] = box[randomIndex];
                box[randomIndex] = temp;

                //box[i].GetComponent<Pickup>().boxId = number;
                //plate[i].GetComponent<Plate>().plateId = number;
                number++;
            }
        }
    }
}