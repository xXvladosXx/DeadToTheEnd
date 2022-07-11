using System;
using System.Collections.Generic;
using GameCore;
using GameCore.Save;
using TMPro;
using UI.Menu.Core;
using UI.Tip;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI.Menu
{
    public class SettingsMenu : Core.Menu
    {
        [SerializeField] private string _warningText;
        
        [SerializeField] private Button _backButton;
        [SerializeField] private Toggle _fullScreen;
        
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;
        
        [SerializeField] private TMP_Dropdown _quality;
        [SerializeField] private TMP_Dropdown _resolutionsDropdown;
        [SerializeField] private RenderPipelineAsset[] _qualityLevels;

        private bool _wereSaved;
        private Resolution[] _resolutions;
        private int _screenInt;
        private bool _isFullScreen;
        private SaveInteractor _saveInteractor;
        
        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            _saveInteractor = saveInteractor;

            _resolutions = Screen.resolutions;
            LoadLastSettings();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            _wereSaved = false;
        }
        
        public void SetEffectsVolume(float volume)
        {
            AudioManager.Instance.ChangeEffectsSound(volume);
            _wereSaved = false;

        }
        public void SetMusicVolume(float volume)
        {
            AudioManager.Instance.ChangeMusicSound(volume);
            _wereSaved = false;

        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = _fullScreen;
            _wereSaved = false;
        }

        public void SetQualityLevelDropdown(int index)
        {
            QualitySettings.SetQualityLevel(index, false);
            _wereSaved = false;
        }
        public void SaveSettings()
        {
            SettingsSaveData settingsSaveData = new SettingsSaveData
            {
                MusicVolume = _musicSlider.value,
                EffectsVolume = _effectsSlider.value,
                Fullscreen = _fullScreen.isOn,
                ResolutionIndex = _resolutionsDropdown.value,
                GraphicsIndex = _quality.value
            };

            _wereSaved = true;
            _saveInteractor.SaveSettings(settingsSaveData);
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(TryToSwitchMenu);
        }

        private void TryToSwitchMenu()
        {
            if (_wereSaved)
            {
                MainMenuSwitcher.ShowLast();
            }
            else
            {
                WarningUI.Instance.ShowWarning(_warningText);
                WarningUI.Instance.OnAccepted += ExitWithoutSaving;
            }
        }

        private void ExitWithoutSaving()
        {
            WarningUI.Instance.OnAccepted -= ExitWithoutSaving;
            LoadLastSettings();
            MainMenuSwitcher.ShowLast();
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
        }
        private void LoadLastSettings()
        {
            var settings = _saveInteractor.LoadSetting();

            FindResolution(settings);
            FindEffectsVolume(settings);
            FindMusicVolume(settings);
            FindFullscreenOption(settings);
            FindGraphicsOption(settings);
            
            _wereSaved = true;
        }

        private void FindGraphicsOption(SettingsSaveData settings)
        {
            _quality.value = settings.GraphicsIndex;
            SetQualityLevelDropdown(settings.GraphicsIndex);
        }

        private void FindEffectsVolume(SettingsSaveData settings)
        {
            _effectsSlider.value = settings.EffectsVolume;
            SetEffectsVolume(settings.EffectsVolume);
        }

        private void FindMusicVolume(SettingsSaveData settings)
        {
            _musicSlider.value = settings.MusicVolume;
            SetMusicVolume(settings.MusicVolume);
        }

        private void FindFullscreenOption(SettingsSaveData settings)
        {
            _fullScreen.isOn = settings.Fullscreen;
            SetFullscreen(_fullScreen.isOn);
        }

        private void FindResolution(SettingsSaveData settings)
        {
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

            _resolutionsDropdown.value = settings.ResolutionIndex;
            _resolutionsDropdown.RefreshShownValue();
            SetResolution(_resolutionsDropdown.value);
        }
    }

  
}