using Interactions;
using ScriptableObjects;
using UnityEngine;

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
        [SerializeField] private float rotationSpeed = 10f;

        private Material _baseMaterial;
        private SoundObject _currentSoundObject;
        private LevelEvent[] _events;
        private LevelController _levelController;
        private MeshRenderer _numberRenderer;

        protected override void Start()
        {
            UseInfo = "Play Current Sound";
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0.8f;
            audioSource.clip = noSoundClip;

            _levelController = GetComponentInParent<LevelController>();
            _events = GetComponents<LevelEvent>();
            soundObjectContainer = transform.GetChild(0).GetComponent<Transform>();

            _numberRenderer = transform.GetChild(2).GetComponent<MeshRenderer>();
            _baseMaterial = _numberRenderer.material;
        }

        private void Update()
        {
            if (_currentSoundObject != null)
                _currentSoundObject.transform.Rotate(new Vector3(1, 2, 5) * (rotationSpeed * Time.deltaTime));
        }

        public bool Validate()
        {
            if (_currentSoundObject == null || _currentSoundObject.note != correctNote) return false;

            _numberRenderer.material = lightMaterial;
            _baseMaterial = lightMaterial;
            return true;
        }

        public void OnPlace(SoundObject soundObject)
        {
            _currentSoundObject = soundObject;

            foreach (var evnt in _events) evnt.Event(soundObject.note);

            _levelController.ValidateSolution();
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
            _numberRenderer.material = lightMaterial;
        }

        public void DisableLight()
        {
            _numberRenderer.material = _baseMaterial;
        }
    }
}