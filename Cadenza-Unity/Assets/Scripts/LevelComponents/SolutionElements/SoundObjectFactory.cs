using System.Collections;
using Event_System;
using LevelComponents.SolutionElements.Buttons;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class SoundObjectFactory : MonoBehaviour
    {
        [SerializeField] private GameObject soundObjectPrefab;
        [SerializeField] private Transform soundObjectHolder;
        [SerializeField] private PitchButton[] pitchButtons;
        [SerializeField] private InstrumentButton[] instrumentButtons;

        private GameObject _soundObject;
        private bool _creatingSoundObject = false;
        
        private void Start()
        {
            pitchButtons = transform.GetChild(1).GetComponentsInChildren<PitchButton>();
            instrumentButtons = transform.GetChild(2).GetComponentsInChildren<InstrumentButton>();

            GameEvents.Current.ONChangeInstrument += OnChangeInstrument;
            GameEvents.Current.ONApplyPitch += OnApplyPitch;
        }

        private void Update()
        {
            if (soundObjectHolder.childCount == 0 && !_creatingSoundObject)
            {
                _creatingSoundObject = true;
                StartCoroutine(CreateNewBall());
            }
        }

        private IEnumerator CreateNewBall()
        {
            yield return new WaitForSeconds(1);
            _soundObject = Instantiate(soundObjectPrefab, soundObjectHolder);
            _creatingSoundObject = false;
        }

        private IEnumerator PlayNewPitch(AudioSource source)
        {
            source.Play();
            yield return new WaitForSeconds(2f);
            source.Stop();
        }

        private void OnApplyPitch(int id, AudioClip clip)
        {
            if (GetInstanceID() != id) return;
            AudioSource source = _soundObject.GetComponent<SoundObject>().aSource;
            source.clip = clip;
            StopCoroutine(PlayNewPitch(source));
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