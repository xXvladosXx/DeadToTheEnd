using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class StartSceneEscapeMenu : EscapeMenu
    {
        [SerializeField] private Button _saveButton;

        protected override void OnEnable()
        {
            base.OnEnable();
            _saveButton.onClick.AddListener(() => MainMenuSwitcher.Show<SaveMenu>());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _saveButton.onClick.RemoveAllListeners();
        }
    }
}