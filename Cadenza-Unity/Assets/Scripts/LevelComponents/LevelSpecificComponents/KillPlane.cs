using System;
using Player;
using UnityEngine;

namespace LevelComponents.LevelSpecificComponents
{
    public class KillPlane : MonoBehaviour
    {
        private PlayerController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_player.gameObject == other.gameObject)
            {
                _player.IsDead = true;
            }
        }
    }
}