using GameCore.Save;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenu : Core.Menu
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _quitButton;

        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            if (saveInteractor.GetLastSave == null)
            {
                _continueButton.gameObject.SetActive(false);
            }
            
            _startButton.onClick.AddListener(() => MainMenuSwitcher.Show<StartMenu>());
            _loadButton.onClick.AddListener(() => MainMenuSwitcher.Show<LoadMenu>());
            _settingButton.onClick.AddListener(() => MainMenuSwitcher.Show<SettingsMenu>());
            _continueButton.onClick.AddListener(() => saveInteractor.ContinueGame(saveInteractor.GetLastSave));
            _quitButton.onClick.AddListener( () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
            });
        }
    }

}
