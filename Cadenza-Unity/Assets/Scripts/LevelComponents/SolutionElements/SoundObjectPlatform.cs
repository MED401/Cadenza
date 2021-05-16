using Interactions;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;

namespace LevelComponents.SolutionElements
{
    public abstract class LevelEvent : MonoBehaviour
    {
        /// <summary>
        ///     The correct not to check against in the Event().
        /// </summary>
        public NoteScriptableObject correctNoteForEvent;

        /// <summary>
        ///     The function which is called when the user places a soundObject on the SoundObjectPlatform.
        /// </summary>
        /// <param name="note">The note from the SoundObject which can be checked against correctNoteForEvent.</param>
        public abstract void Event(NoteScriptableObject note);
    }

    public class SoundObjectPlatform : Interactable
    {
        public NoteScriptableObject correctNote;
        public Transform soundObjectContainer;
        public AudioSource audioSource;

        [SerializeField] private AudioClip noSoundClip;
        [SerializeField] private Material lightMaterial;
        [SerializeField] private Material altLightMaterial;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        private AudioReverbZone _audioReverbZone;
        private Material _baseMaterial;
        private SoundObject _currentSoundObject;
        private LevelEvent[] _events;
        private bool _lightOn, _altLightOn;
        private MeshRenderer _numberRenderer;

        public bool PlayingSound { get; set; }
        public bool HasValidSolution { get; private set; }

        protected override void Start()
        {
            UseInfo = "Play Current Sound";
            audioSource = gameObject.AddComponent<AudioSource>();
            _audioReverbZone = gameObject.AddComponent<AudioReverbZone>();
            _audioReverbZone.minDistance = 20;
            _audioReverbZone.maxDistance = 100;
            _audioReverbZone.reverbPreset = AudioReverbPreset.ParkingLot;
            audioSource.spatialBlend = 0.8f;
            audioSource.clip = noSoundClip;
            audioSource.outputAudioMixerGroup = audioMixerGroup;

            audioSource.maxDistance = 100;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0;

            _events = GetComponents<LevelEvent>();
            soundObjectContainer = transform.GetChild(0).GetComponent<Transform>();

            _numberRenderer = transform.GetChild(2).GetComponent<MeshRenderer>();
            _baseMaterial = _numberRenderer.material;
        }

        private void Update()
        {
            _currentSoundObject = soundObjectContainer.childCount == 0
                ? null
                : soundObjectContainer.GetChild(0).GetComponent<SoundObject>();

            if (_currentSoundObject)
                _currentSoundObject.transform.Rotate(new Vector3(1, 2, 5) * (rotationSpeed * Time.deltaTime));


            if (PlayingSound || _lightOn)
                _numberRenderer.material = lightMaterial;
            else if (_altLightOn)
                _numberRenderer.material = altLightMaterial;
            else
                _numberRenderer.material = _baseMaterial;

            Validate();
        }

        private void Validate()
        {
            if (!_currentSoundObject || _currentSoundObject.note != correctNote)
            {
                _lightOn = false;
                HasValidSolution = false;
            }
            else
            {
                _lightOn = true;
                HasValidSolution = true;
            }
        }

        public void OnPlace(SoundObject soundObject)
        {
            foreach (var evnt in _events) evnt.Event(soundObject.note);
        }

        public override void Interact()
        {
            if (_currentSoundObject == null)
            {
                audioSource.clip = noSoundClip;
                audioSource.Play();
            }
            else
            {
                _currentSoundObject.PlaySound();
            }
        }

        public void EnableAltLight()
        {
            _altLightOn = true;
        }
    }
}