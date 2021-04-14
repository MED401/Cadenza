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
        [SerializeField] private AudioClip noSoundClip;

        private LevelEvent[] _events;
        private LevelController _levelController;
        private AudioSource _noSound;

        public Transform SoundObjectContainer { get; set; }
        private SoundObject CurrentSoundObject { get; set; }

        protected override void Start()
        {
            UseInfo = "Play Current Sound";
            _noSound = gameObject.AddComponent<AudioSource>();
            _noSound.spatialBlend = 0.8f;
            _noSound.clip = noSoundClip;

            _levelController = GetComponentInParent<LevelController>();
            _events = GetComponents<LevelEvent>();
            SoundObjectContainer = transform.GetChild(0).GetComponent<Transform>();
        }

        public bool Validate()
        {
            if (CurrentSoundObject == null) return false;
            return CurrentSoundObject.note == correctNote;
        }

        public void OnPlace(SoundObject soundObject)
        {
            CurrentSoundObject = soundObject;

            foreach (var evnt in _events) evnt.Event(soundObject.note);

            _levelController.ValidateSolution();
        }


        public override void Interact()
        {
            if (CurrentSoundObject == null) _noSound.Play();

            else CurrentSoundObject.PlaySound();
        }
    }
}