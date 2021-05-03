using UnityEngine;
using UnityEngine.Audio;

namespace Menus
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("masterVolume", volume);
        }

        public void SetAmbientVolume(float volume)
        {
            audioMixer.SetFloat("ambientVolume", volume);
        }

        public void SetSoundObjectVolume(float volume)
        {
             audioMixer.SetFloat("soundObjectVolume", volume);
        }
    }
}