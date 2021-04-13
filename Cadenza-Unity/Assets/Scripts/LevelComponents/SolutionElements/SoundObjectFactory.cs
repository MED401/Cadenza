using System.Collections;
using LevelComponents.SolutionElements.Buttons;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class SoundObjectFactory : MonoBehaviour
    {
        [SerializeField] private GameObject soundObjectPrefab;
        [SerializeField] private Transform soundObjectHolder;
        
        private PitchSelector[] _pitchButtons;
        private bool _creatingSoundObject;
        private SoundObject _soundObject;

        private void Start()
        {
            _pitchButtons = transform.GetChild(1).GetComponentsInChildren<PitchSelector>();
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
            _soundObject = Instantiate(soundObjectPrefab, soundObjectHolder).GetComponent<SoundObject>();
            _creatingSoundObject = false;
        }

        public void SetPitch(AudioClip clip)
        {
            _soundObject.ASource.clip = clip;
            _soundObject.PlaySound();
        }

        public void SetInstrument(string path)
        { 
            var index = 1;
            foreach (var btn in _pitchButtons)
            {
                btn.Clip = Resources.Load<AudioClip>(path + "/" + index); 
                index++;
            }

            _soundObject.ASource.clip = _pitchButtons[2].Clip;
            _soundObject.PlaySound();
        }
    }
}