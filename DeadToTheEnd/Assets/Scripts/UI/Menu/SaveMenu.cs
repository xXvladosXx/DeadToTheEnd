using GameCore;
using GameCore.Save;
using TMPro;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class SaveMenu: Core.Menu
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _saveNewGameButton;
        [SerializeField] private Transform _content;
        public override void Initialize(SaveInteractor saveInteractor)
        {
            base.Initialize(saveInteractor);
            _backButton.onClick.AddListener(MainMenuSwitcher.ShowLast);
            _saveNewGameButton.onClick.AddListener(() => MainMenuSwitcher.Show<CreateGameMenu>());
            
            OnEnable();
        }
        
        private void OnEnable()
        {
            if(SaveInteractor == null) return;
            
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var save in SaveInteractor.SaveList())
            {
                Button savePrefab = Instantiate(_saveButton, _content);
                savePrefab.GetComponentInChildren<TextMeshProUGUI>().text = save;
            
                savePrefab.onClick.AddListener((() => { SaveInteractor.Save(save); }));
            }
        }

        
    }
}