using System.Collections;
using LevelComponents.SolutionElements.Buttons;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class SoundObjectFactory : MonoBehaviour
    {
        [SerializeField] private GameObject soundObjectPrefab;
        [SerializeField] private Transform soundObjectHolder;
        [SerializeField] private float rotationSpeed = 10f; 
        private bool _creatingSoundObject;

        private PitchSelector[] _pitchButtons;
        private SoundObject _soundObject;

        private void Start()
        {
            _pitchButtons = transform.GetChild(1).GetComponentsInChildren<PitchSelector>();
        }

        private void Update()
        {
            if (soundObjectHolder.childCount == 0 && !_creatingSoundObject) StartCoroutine(CreateNewBall());

            if (_soundObject != null) _soundObject.transform.Rotate(new Vector3(1, 2, 5) * (rotationSpeed * Time.deltaTime));
    }

        private IEnumerator CreateNewBall()
        {
            _creatingSoundObject = true;
            yield return new WaitForSeconds(1);
            _soundObject = Instantiate(soundObjectPrefab, soundObjectHolder).GetComponent<SoundObject>();
            _creatingSoundObject = false;
        }

        public void SetPitch(NoteScriptableObject aNote)
        {
            _soundObject.note = aNote;
            _soundObject.PlaySound();
        }

        public void SetInstrument(InstrumentScriptableObject instrument)
        {
            for (var i = 0; i < _pitchButtons.Length; i++) _pitchButtons[i].note = instrument.notes[i];

            var mesh = _soundObject.GetComponent<MeshRenderer>();
            mesh.materials = new[] {mesh.material, instrument.material};
            
            _soundObject.note = instrument.notes[2];
            _soundObject.PlaySound();
        }
    }
}