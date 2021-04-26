using System.Collections;
using System.Collections.Generic;
using Interactions;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.WinterWonderlandPillars
{
    public class PillarSolution : Interactable
    {
        private AudioSource _audioSource;
        private LevelController _levelController;
        [SerializeField] private int pillarPedestal;
        [SerializeField] private NoteScriptableObject _correctNote;


        protected override void Start()
        {
            UseInfo = "Play Target Sound";
            _levelController = GetComponentInParent<LevelController>();
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public override void Interact()
        {
            StartCoroutine(PlayCorrectSounds(_correctNote));
        }

        private IEnumerator PlayCorrectSounds(NoteScriptableObject sound)
        {
            _levelController.soundObjectPlatforms[pillarPedestal-1].EnableLight();

                _audioSource.clip = sound.clip;
                _audioSource.Play();
                yield return new WaitForSeconds(2);
            

            _audioSource.Stop();
            _levelController.soundObjectPlatforms[pillarPedestal-1].DisableLight();
        }
    }
}