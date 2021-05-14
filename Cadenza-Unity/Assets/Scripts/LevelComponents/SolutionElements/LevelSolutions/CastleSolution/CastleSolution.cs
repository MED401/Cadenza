﻿using SceneManagement;
using UnityEngine;

namespace LevelComponents.SolutionElements.LevelSolutions.CastleSolution
{
    public class CastleSolution : LevelSolutionEvent
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject door;

        public override void OnLevelSolution()
        {
            audioSource.Play();
            door.SetActive(false);
        }

        public override void OnNoLevelSolution()
        {
            audioSource.Play();
            door.SetActive(true);
        }
    }
}