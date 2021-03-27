using Event_System;
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

        private void OnSoundObjectPickUp(int id) //Hertil
        {
            Debug.Log("Goodbye");
            if (soundObject.transform.GetInstanceID() != id)

                Debug.Log("Hello");
            //Instantiate(soundObject.gameObject, soundObjectHolder);
            Instantiate(soundObject.gameObject, new Vector3(0,2,0), quaternion.identity, soundObjectHolder);
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