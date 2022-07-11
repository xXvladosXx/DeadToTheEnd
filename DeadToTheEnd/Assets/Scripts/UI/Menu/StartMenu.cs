using System;
using System.Text;
using GameCore;
using GameCore.Save;
using TMPro;
using UI.Menu.Core;
using UI.Tip;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class StartMenu : Core.Menu
    {
        [SerializeField] private string _warningText;
        [SerializeField] private string _sameGameWarningText;
        
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startNewGame;
        [SerializeField] private TMP_InputField _inputField;


        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            _startNewGame.onClick.AddListener(TryToStartNewGame);
            _backButton.onClick.AddListener(MainMenuSwitcher.ShowLast);
        }

        private void TryToStartNewGame()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(_warningText).Append("<i>").Append($" {SaveFile}").Append("</i>").Append("?");
            
            foreach (var saving in SaveInteractor.SaveList())
            {
                if (saving == SaveFile)
                {
                    stringBuilder.Clear();
                    stringBuilder.Append(_sameGameWarningText).Append("<i>").Append($" {SaveFile}").Append("</i>").Append("?");
                }
            }
            
            WarningUI.Instance.ShowWarning(stringBuilder.ToString());
            
            WarningUI.Instance.OnAccepted += StartNewGame;
        }

        public void CreateName(string saveFile)
        {
            SaveFile = saveFile;
        }

        private void OnDisable()
        {
            _inputField.text = string.Empty;
            SaveFile = string.Empty;
        }

        private void StartNewGame()
        {
            WarningUI.Instance.OnAccepted -= StartNewGame;
            SaveInteractor.StartNewGame(SaveFile);
        }
    }
}