using System;
using System.Collections.Generic;
using Event_System;
using UnityEngine;

namespace SoundMachine
{
    public class SoundBox : MonoBehaviour
    {
        [SerializeField] private SoundObject soundObject;
        [SerializeField] private Transform soundObjectHolder;
        public AudioSource[] sounds;

        private List<Button> instrumentButtons;
        private List<Button> pitchButtons;

        private void Start()
        {
            foreach (PitchButton btn in transform.GetChild(1)) pitchButtons.Add(btn);
            foreach (InstrumentButton btn in transform.GetChild(2)) instrumentButtons.Add(btn);

            GameEvents.Current.OnChangeInstrument += OnChangeInstrument;
            GameEvents.Current.OnApplyPitch += OnApplyPitch;
        }

        private void OnApplyPitch(int id, AudioClip clip)
        {
            throw new NotImplementedException();
        }

        private void OnChangeInstrument(int id, string path)
        {
            if (GetInstanceID() != id) return;

            var index = 0;
            foreach (PitchButton btn in pitchButtons)
            {
                btn.clip = Resources.Load<AudioClip>(path + "/" + index);
                ++index;
            }
        }
    }
}