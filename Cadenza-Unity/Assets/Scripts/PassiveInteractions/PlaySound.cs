using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public AudioSource audio;
    public bool alreadyPlayed = false;

    public AudioSource Door;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if (!alreadyPlayed)
        {
            audio.Play();
            Door.Play();
            alreadyPlayed = true;
        }
    }
}