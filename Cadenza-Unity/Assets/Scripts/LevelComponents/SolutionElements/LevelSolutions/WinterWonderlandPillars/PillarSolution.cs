using System.Collections;
using Interactions;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.WinterWonderlandPillars
{
    public class PillarSolution : Interactable
    {
        [SerializeField] private int pillarPedestal;
        [SerializeField] private NoteScriptableObject correctNote;
        private AudioSource _audioSource;
        private LevelController _levelController;


        protected override void Start()
        {
            UseInfo = "Play Target Sound";
            _levelController = GetComponentInParent<LevelController>();
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public override void Interact()
        {
            StartCoroutine(PlayCorrectSounds(correctNote));
        }

        private IEnumerator PlayCorrectSounds(NoteScriptableObject sound)
        {
            _levelController.soundObjectPlatforms[pillarPedestal - 1].PlayingSound = true;

            _audioSource.clip = sound.clip;
            _audioSource.Play();
            yield return new WaitForSeconds(2);

            _audioSource.Stop();
            _levelController.soundObjectPlatforms[pillarPedestal - 1].PlayingSound = false;
        }
    }
}