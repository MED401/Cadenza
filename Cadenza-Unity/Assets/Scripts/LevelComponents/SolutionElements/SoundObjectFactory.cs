using System.Collections;
using LevelComponents.SolutionElements.Buttons;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements
{
    public class SoundObjectFactory : MonoBehaviour
    {
        [SerializeField] private NoteScriptableObject missingNoteScriptableObject;
        [SerializeField] private GameObject soundObjectPrefab;
        [SerializeField] private Transform soundObjectHolder;
        [SerializeField] private float rotationSpeed = 10f;

        private bool _creatingSoundObject;
        private Material _instrumentMaterial;
        private PitchSelector[] _pitchButtons;
        private SoundObject _soundObject;

        private void Start()
        {
            _pitchButtons = transform.GetChild(1).GetComponentsInChildren<PitchSelector>();
        }

        private void Update()
        {
            if (soundObjectHolder.childCount == 0 && !_creatingSoundObject) StartCoroutine(CreateNewBall());

            if (_soundObject != null)
                _soundObject.transform.Rotate(new Vector3(1, 2, 5) * (rotationSpeed * Time.deltaTime));
        }

        private IEnumerator CreateNewBall()
        {
            _soundObject = null;
            _creatingSoundObject = true;
            yield return new WaitForSeconds(1);
            _soundObject = Instantiate(soundObjectPrefab, soundObjectHolder).GetComponent<SoundObject>();

            _creatingSoundObject = false;
        }

        public void SetPitch(NoteScriptableObject aNote)
        {
            if (aNote == null)
            {
                _soundObject.note = missingNoteScriptableObject;
                _soundObject.PlaySound();
            }
            else
            {
                _soundObject.note = aNote;
                _soundObject.PlaySound();

                var mesh = _soundObject.GetComponent<MeshRenderer>();
                mesh.materials = new[] {mesh.material, _instrumentMaterial};
            }
        }

        public void SetInstrument(InstrumentScriptableObject instrument)
        {
            for (var i = 0; i < _pitchButtons.Length; i++) _pitchButtons[i].note = instrument.notes[i];

            var mesh = _soundObject.GetComponent<MeshRenderer>();
            mesh.materials = new[] {mesh.material, instrument.material};
            _instrumentMaterial = instrument.material;

            _soundObject.note = instrument.notes[2];
            _soundObject.PlaySound();
        }
    }
}