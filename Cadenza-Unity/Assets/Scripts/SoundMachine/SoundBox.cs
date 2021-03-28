using System;
using System.Collections;
using System.Collections.Generic;
using Event_System;
using Interactions;
using Unity.Mathematics;
using UnityEngine;

namespace SoundMachine
{
    public class SoundBox : MonoBehaviour
    {
        [SerializeField] private GameObject soundObject;
        [SerializeField] private Transform soundObjectHolder;


        [SerializeField] private PitchButton[] pitchButtons;
        [SerializeField] private InstrumentButton[] instrumentButtons;

        private void Start()
        {
            pitchButtons = transform.GetChild(1).GetComponentsInChildren<PitchButton>();
            instrumentButtons = transform.GetChild(2).GetComponentsInChildren<InstrumentButton>();

            GameEvents.Current.OnChangeInstrument += OnChangeInstrument;
            GameEvents.Current.OnApplyPitch += OnApplyPitch;
            GameEvents.Current.ONInteract += OnSoundObjectPickUp;
           
        }

        private void OnSoundObjectPickUp(int id)
        {
            /*if (soundObjectHolder.childCount > 0)
            {
                return;
            }
            else
            {
                GameObject newBall = Instantiate(soundObject, soundObjectHolder); 
                newBall.SetActive(true);
            }*/

            StartCoroutine(CreateNewBall());
        }

        private IEnumerator CreateNewBall()
        {
            yield return new WaitForSeconds(1);
            GameObject newBall = Instantiate(soundObject, soundObjectHolder); 
                newBall.SetActive(true);
        }

        private void OnApplyPitch(int id, AudioClip clip)
        {
            if (GetInstanceID() != id) return;

            //soundObject.SoundSource.clip = clip; 
            //soundObject.SoundSource.Play();
        }

        private void OnChangeInstrument(int id, string path)
        {
            if (GetInstanceID() != id) return;

            var index = 1;
            foreach (var btn in pitchButtons)
            {
                btn.Clip = Resources.Load<AudioClip>(path + index);
                ++index;
            }
        }
    }
}