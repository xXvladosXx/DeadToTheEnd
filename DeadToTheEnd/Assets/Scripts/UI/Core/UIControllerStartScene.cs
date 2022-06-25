using GameCore;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIControllerStartScene : UIController
    {
        [SerializeField] private Button _inventory;
        [SerializeField] private Button _shop;
        [SerializeField] private Button _skills;
        [SerializeField] private Button _quests;
        [SerializeField] private Button _fight;

        protected override void Init(InteractorsBase interactorsBase)
        {
            base.Init(interactorsBase);
            _inventory.onClick.AddListener(SwitchUIElement<CharacterContainerUI>);
            _shop.onClick.AddListener(SwitchUIElement<ShopUI>);
            _skills.onClick.AddListener(SwitchUIElement<SkillsUI>);
            _quests.onClick.AddListener(SwitchUIElement<QuestsUI>);
            _fight.onClick.AddListener(SwitchUIElement<FightUI>);

            CreateInputs();
        }

        private void CreateInputs()
        {
            MainPlayer.InputAction.BarActions.Fight.performed += context => { SwitchUIElement<FightUI>(); };
            MainPlayer.InputAction.BarActions.Quests.performed += context => { SwitchUIElement<QuestsUI>(); };
            MainPlayer.InputAction.BarActions.Skills.performed += context => { SwitchUIElement<SkillsUI>(); };
            MainPlayer.InputAction.BarActions.Shop.performed += context => { SwitchUIElement<ShopUI>(); };
            MainPlayer.InputAction.BarActions.Inventory.performed += context => { SwitchUIElement<CharacterContainerUI>(); };
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