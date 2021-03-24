using System.Linq;
using UnityEngine;

namespace SoundMachine
{
    public class SoundBox : MonoBehaviour
    {
        [SerializeField] private SoundObject soundObject;
        [SerializeField] private Transform soundObjectHolder;
        public AudioSource[] sounds;

        private Button[] instrumentButtons;
        private Button[] pitchButtons;

        private void Start()
        {
            
            
        }
    }
}