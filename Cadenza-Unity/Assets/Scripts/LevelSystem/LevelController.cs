using System;
using UnityEngine;

namespace LevelSystem
{
    public class LevelController : MonoBehaviour
    {
        private SoundObjectPlatform[] soundObjectPlatforms;

        private void Start()
        {
            soundObjectPlatforms = GetComponentsInChildren<SoundObjectPlatform>(); 
        }
    }
}
