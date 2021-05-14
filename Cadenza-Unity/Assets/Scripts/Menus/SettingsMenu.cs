using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Player;

namespace Menus
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public Dropdown resolutionDropDown, graphicsQualityDropDown;
        public Toggle fullscreenToggle;
        public Slider masterVolumeSlider, ambientVolumeSlider, soundObjectVolumeSlider, mouseSensitivitySlider;

        private Resolution[] _resolutions;
        private PlayerController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            _resolutions = Screen.resolutions;
            resolutionDropDown.ClearOptions();
            var resolutionOptions = new List<string>();

            var currentResolutionIndex = 0;

            for (var i = 0; i < _resolutions.Length; i++)
            {
                var option = _resolutions[i].width + " x " + _resolutions[i].height;
                resolutionOptions.Add(option);

                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
            }

            resolutionDropDown.AddOptions(resolutionOptions);
            resolutionDropDown.value = currentResolutionIndex;
            resolutionDropDown.RefreshShownValue();

            if (CurrentAudioSettings.MasterVolume != 0) masterVolumeSlider.value = CurrentAudioSettings.MasterVolume;
            if (CurrentAudioSettings.AmbientVolume != 0) ambientVolumeSlider.value = CurrentAudioSettings.AmbientVolume;
            if (CurrentAudioSettings.SoundObjectVolume != 0) soundObjectVolumeSlider.value = CurrentAudioSettings.SoundObjectVolume;
            if (CurrentAudioSettings.MouseSensitivity != 0) mouseSensitivitySlider.value = CurrentAudioSettings.MouseSensitivity;

            graphicsQualityDropDown.value = QualitySettings.GetQualityLevel();
            fullscreenToggle.isOn = Screen.fullScreen;
        }

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
            CurrentAudioSettings.MasterVolume = volume;
        }

        public void SetAmbientVolume(float volume)
        {
            audioMixer.SetFloat("ambientVolume", Mathf.Log10(volume) * 20);
            CurrentAudioSettings.AmbientVolume = volume;
        }

        public void SetSoundObjectVolume(float volume)
        {
            audioMixer.SetFloat("soundObjectVolume", Mathf.Log10(volume) * 20);
            CurrentAudioSettings.SoundObjectVolume = volume;
        }

        public void SetResolution(int resolutionIndex)
        {
            var resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetMouseSensitivity(float sensitivity)
        {
            if (_player != null) _player.mouseSensitivity = sensitivity;
            CurrentAudioSettings.MouseSensitivity = sensitivity;
        }
    }
}