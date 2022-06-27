using GameCore;
using GameCore.Save;
using TMPro;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class LoadMenu : Core.Menu
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Transform _content;
        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            _backButton.onClick.AddListener(MainMenuSwitcher.ShowLast);
            
            foreach (var save in SaveInteractor.SaveList())
            {
                Button loadPrefab = Instantiate(_loadButton, _content);
                loadPrefab.GetComponentInChildren<TextMeshProUGUI>().text = save;
            
                loadPrefab.onClick.AddListener((() => { SaveInteractor.LoadGame(save); }));
            }
        }
        private void OnEnable()
        {
            
        }
    }
}