using System.Collections;
using LevelComponents.SolutionElements.Buttons;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class SoundObjectFactory : MonoBehaviour
    {
        [SerializeField] private GameObject soundObjectPrefab;
        [SerializeField] private Transform soundObjectHolder;
        [SerializeField] private PitchSelector[] pitchButtons;
        [SerializeField] private InstrumentSelector[] instrumentButtons;
        private bool _creatingSoundObject;

        private SoundObject _soundObject;

        private void Start()
        {
            pitchButtons = transform.GetChild(1).GetComponentsInChildren<PitchSelector>();
            instrumentButtons = transform.GetChild(2).GetComponentsInChildren<InstrumentSelector>();
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
            foreach (var btn in pitchButtons)
            {
                btn.Clip = Resources.Load<AudioClip>(path + "/" + index); 
                index++;
            }

            _soundObject.ASource.clip = pitchButtons[2].Clip;
            _soundObject.PlaySound();
        }

        private static IEnumerator PlayNewPitch(AudioSource source)
        {
            source.Play();
            yield return new WaitForSeconds(2f);
            source.Stop();
        }
    }
}