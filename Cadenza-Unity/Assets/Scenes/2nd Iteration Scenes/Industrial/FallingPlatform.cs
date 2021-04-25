using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public float speed = 10;
    public float timer = 1;
    public float respawnTimer = 5;
    private bool timerOn = false;
    private float ownTimer = 0;
    private GameObject platform;
    private Vector3 startPos; 

    void Start()
    {
        platform = this.transform.parent.gameObject;
        //platform = GetComponentInParent<Transform>();
        startPos = platform.transform.position;
        //Debug.Log("Y" + startY);
    }

    void Update()
    {

        if (ownTimer > respawnTimer + timer)
        {
            timerOn = false;
            platform.transform.position = startPos;
            ownTimer = 0;
        }

        if (ownTimer > timer && timerOn)
        {
            platform.transform.Translate(Vector3.down * speed * Time.deltaTime);

        } 
        
        if (timerOn)
        {
            ownTimer += Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("I am hitting something");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("I should be falling any second now");
            startTimer();
        }
    }

    void startTimer()
    {
        timerOn = true;
    }

}
