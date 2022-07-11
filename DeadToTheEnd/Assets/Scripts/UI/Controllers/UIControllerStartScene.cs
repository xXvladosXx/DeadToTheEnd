using GameCore;
using UI.Fight;
using UI.Inventory.ItemContainers;
using UI.Inventory.ItemContainers.Core;
using UI.Menu;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Controllers
{
    public class UIControllerStartScene : UIController
    {
        [SerializeField] private Button _inventory;
        [SerializeField] private Button _shop;
        [SerializeField] private Button _skills;
        [SerializeField] private Button _quests;
        [SerializeField] private Button _fight;
        [SerializeField] private Button _menu;

        protected override void Init(InteractorsBase interactorsBase)
        {
            base.Init(interactorsBase);
            
            _inventory.onClick.AddListener(SwitchUIElement<CharacterContainerUI>);
            _shop.onClick.AddListener(SwitchUIElement<ShopUI>);
            _skills.onClick.AddListener(SwitchUIElement<SkillsUI>);
            _quests.onClick.AddListener(SwitchUIElement<QuestsUI>);
            _fight.onClick.AddListener(SwitchUIElement<FightUI>);
            _menu.onClick.AddListener(SwitchUIElement<MainMenuSwitcher>);

            CreateInputs();
        }

        private void CreateInputs()
        {
            GameInput.InputAction.Bar.Fight.performed += context => { SwitchUIElement<FightUI>(); };
            GameInput.InputAction.Bar.Quests.performed += context => { SwitchUIElement<QuestsUI>(); };
            GameInput.InputAction.Bar.Skills.performed += context => { SwitchUIElement<SkillsUI>(); };
            GameInput.InputAction.Bar.Shop.performed += context => { SwitchUIElement<ShopUI>(); };
            GameInput.InputAction.Bar.Inventory.performed += context => { SwitchUIElement<CharacterContainerUI>(); };
            GameInput.InputAction.Bar.Menu.performed += TryToSwitchToMenu;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inventory.onClick.RemoveAllListeners();
            _shop.onClick.RemoveAllListeners();
            _skills.onClick.RemoveAllListeners();
            _quests.onClick.RemoveAllListeners();
            _fight.onClick.RemoveAllListeners();
        }
    }
}