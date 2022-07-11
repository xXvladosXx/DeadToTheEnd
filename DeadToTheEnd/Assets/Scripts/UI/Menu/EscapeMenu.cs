using System;
using GameCore;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class EscapeMenu : Core.Menu
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _quitButton;

        protected virtual void OnEnable()
        {
            _loadButton.onClick.AddListener(() => MainMenuSwitcher.Show<LoadMenu>());
            _settingButton.onClick.AddListener(() => MainMenuSwitcher.Show<SettingsMenu>());
            _quitButton.onClick.AddListener( () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
            });
        }

        protected virtual void OnDisable()
        {
            _loadButton.onClick.RemoveAllListeners();
            _settingButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }
    }
}