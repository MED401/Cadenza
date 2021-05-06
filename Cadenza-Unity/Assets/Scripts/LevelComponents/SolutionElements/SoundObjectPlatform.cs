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
        public AudioReverbZone audioReverbZone;

        [SerializeField] private AudioClip noSoundClip;
        [SerializeField] private Material lightMaterial;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        
        private Material _baseMaterial;
        private Material _currentMaterial;
        private SoundObject _currentSoundObject;
        private LevelEvent[] _events;
        private bool _lightOn;
        private MeshRenderer _numberRenderer;

        public bool HasValidSolution { get; private set; }

        protected override void Start()
        {
            UseInfo = "Play Current Sound";
            audioSource = gameObject.AddComponent<AudioSource>();
            audioReverbZone = gameObject.AddComponent<AudioReverbZone>();
            audioReverbZone.minDistance = 20;
            audioReverbZone.maxDistance = 135;
            audioReverbZone.reverbPreset = AudioReverbPreset.ParkingLot; 
            audioSource.spatialBlend = 1f;
            audioSource.clip = noSoundClip;
            audioSource.outputAudioMixerGroup = audioMixerGroup;

            audioSource.maxDistance = 135;
            audioSource.rolloffMode = AudioRolloffMode.Linear;

            _events = GetComponents<LevelEvent>();
            soundObjectContainer = transform.GetChild(0).GetComponent<Transform>();

            _numberRenderer = transform.GetChild(2).GetComponent<MeshRenderer>();
            _baseMaterial = _numberRenderer.material;
            _currentMaterial = _baseMaterial;
        }

        private void Update()
        {
            _currentSoundObject = soundObjectContainer.childCount == 0
                ? null
                : soundObjectContainer.GetChild(0).GetComponent<SoundObject>();

            if (_currentSoundObject)
                _currentSoundObject.transform.Rotate(new Vector3(1, 2, 5) * (rotationSpeed * Time.deltaTime));

            _numberRenderer.material = _lightOn ? lightMaterial : _currentMaterial;

            Validate();
        }

        private void Validate()
        {
            if (!_currentSoundObject || _currentSoundObject.note != correctNote)
            {
                _currentMaterial = _baseMaterial;
                HasValidSolution = false;
            }
            else
            {
                _currentMaterial = lightMaterial;
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

        public void EnableLight()
        {
            _lightOn = true;
        }

        public void DisableLight()
        {
            _lightOn = false;
        }
    }
}