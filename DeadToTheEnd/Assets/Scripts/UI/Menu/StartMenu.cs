using GameCore;
using GameCore.Save;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class StartMenu : Core.Menu
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startNewGame;

        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            
            _backButton.onClick.AddListener(MainMenuSwitcher.ShowLast);
            _startNewGame.onClick.AddListener(StartNewGame);
        }
        
        public void CreateName(string saveFile)
        {
            SaveFile = saveFile;
        }
        
        private void StartNewGame()
        {
            SaveInteractor.StartNewGame(SaveFile);
        }
    }
}