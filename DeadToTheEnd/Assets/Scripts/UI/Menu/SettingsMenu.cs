using System.Collections.Generic;
using GameCore;
using GameCore.Save;
using TMPro;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI.Menu
{
    public class SettingsMenu : Core.Menu
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Toggle _fullScreen;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TMP_Dropdown _quality;
        [SerializeField] private TMP_Dropdown _resolutionsDropdown;
        [SerializeField] private RenderPipelineAsset[] _qualityLevels;
        
        [SerializeField] private AudioMixer _musicMixer;

        private Resolution[] _resolutions;
        private int _screenInt;
        private bool _isFullScreen;
        
        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            _backButton.onClick.AddListener(MainMenuSwitcher.ShowLast);

            _resolutions = Screen.resolutions;
            
            _resolutionsDropdown.ClearOptions();
            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            
            _resolutionsDropdown.AddOptions(options);
            _resolutionsDropdown.value = currentResolutionIndex;
            _resolutionsDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        
        public void SetVolume(float volume)
        {
            _musicMixer.SetFloat("volume", volume);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = _fullScreen;
        }
    }
}