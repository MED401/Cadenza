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
        [SerializeField] private GameObject soundObjectPrefab;
        [SerializeField] private Transform soundObjectHolder;
        [SerializeField] private PitchButton[] pitchButtons;
        [SerializeField] private InstrumentButton[] instrumentButtons;

        private GameObject soundObject;

        private void Start()
        {
            pitchButtons = transform.GetChild(1).GetComponentsInChildren<PitchButton>();
            instrumentButtons = transform.GetChild(2).GetComponentsInChildren<InstrumentButton>();

            GameEvents.Current.ONChangeInstrument += OnChangeInstrument;
            GameEvents.Current.ONApplyPitch += OnApplyPitch;
            GameEvents.Current.ONInteract += OnSoundObjectPickUp;
            StartCoroutine(CreateNewBall());
        }

        private void OnSoundObjectPickUp(int id)
        {
            if (soundObjectHolder.transform.GetComponentInChildren<SoundObject>().GetInstanceID() != id) return;
            StartCoroutine(CreateNewBall());
        }

        private IEnumerator CreateNewBall()
        {
            yield return new WaitForSeconds(1);
            soundObject = Instantiate(soundObjectPrefab, soundObjectHolder);
        }

        private IEnumerator PlayNewPitch(AudioSource source)
        {
            source.Play();
            yield return new WaitForSeconds(1.2f);
            source.Stop();
        }

        private void OnApplyPitch(int id, AudioClip clip)
        {
            if (GetInstanceID() != id) return;
            AudioSource source = soundObject.GetComponent<SoundObject>().aSource;
            source.clip = clip;
            source.spatialBlend = 0.8f; 
            StartCoroutine(PlayNewPitch(source));
        }

        private void OnChangeInstrument(int id, string path)
        {
            if (GetInstanceID() != id) return;
            
            var index = 1;
            foreach (var btn in pitchButtons)
            {
                btn.Clip = Resources.Load<AudioClip>(path + "/" + index);
                ++index;
            }
        }
    }
}