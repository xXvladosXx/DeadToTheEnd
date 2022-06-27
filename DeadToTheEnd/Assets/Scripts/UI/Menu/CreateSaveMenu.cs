using GameCore.Save;
using TMPro;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class CreateSaveMenu : Core.Menu
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startNewGame;
        [SerializeField] private TextMeshProUGUI _warning;
        
        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            _backButton.onClick.AddListener(MainMenuSwitcher.ShowLast);
            _startNewGame.onClick.AddListener(SaveNewGame);
        }
        
        public void CreateName(string saveFile)
        {
            SaveFile = saveFile;
        }

        private void SaveNewGame()
        {
            if (SaveFile.Length > 0)
            {
                _warning.text = "";
                SaveInteractor.Save(SaveFile);
                MainMenuSwitcher.ShowLast();
            }
            else
            {
                _warning.text = "Fill the name";
            }
        }
    }
}