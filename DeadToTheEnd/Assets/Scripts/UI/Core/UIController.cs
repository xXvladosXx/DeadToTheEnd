using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private MainPlayer _mainPlayer;

        [SerializeField] private Button _inventory;
        [SerializeField] private Button _shop;
        [SerializeField] private Button _skills;
        [SerializeField] private Button _quests;
        [SerializeField] private Button _fight;

        private List<UIElement> _uiElements;
        private UIElement _currentUIElement;

        private void Awake()
        {
            _uiElements = GetComponentsInChildren<UIElement>().ToList();
            foreach (var uiElement in _uiElements)
            {
                uiElement.Hide();
            }

            _inventory.onClick.AddListener(SwitchUIElement<CharacterContainerUI>);
            _shop.onClick.AddListener(SwitchUIElement<ShopUI>);
        }

        private void Start()
        {
            ReadInventoryInput();
            ReadShopInput();
            ReadSkillsInput();
            ReadQuestsInput();
            ReadFightInput();
        }

        private void ReadFightInput()
        {
            _mainPlayer.InputAction.BarActions.Fight.performed += context =>
            {
                SwitchUIElement<FightUI>();

            };
        }

        private void ReadQuestsInput()
        {
            _mainPlayer.InputAction.BarActions.Quests.performed += context =>
            {
                SwitchUIElement<QuestsUI>();

            };
        }

        private void ReadSkillsInput()
        {
            _mainPlayer.InputAction.BarActions.Skills.performed += context =>
            {
                SwitchUIElement<SkillsUI>();

            };
        }

        private void ReadShopInput()
        {
            _mainPlayer.InputAction.BarActions.Shop.performed += context =>
            {
                SwitchUIElement<ShopUI>();

            };
        }

        private void ReadInventoryInput()
        {
            _mainPlayer.InputAction.BarActions.Inventory.performed += context =>
            {
                    SwitchUIElement<CharacterContainerUI>();
            };
        }

        private void SwitchUIElement<T>() where T : UIElement
        {
            if (_currentUIElement != null)
            {
                if (_currentUIElement.GetType() == typeof(T))
                {
                    HideUIElement();
                    return;
                }

                HideUIElement();
            }

            ShowUIElement<T>();
        }

        private void ShowUIElement<T>() where T : UIElement
        {
            var uiElement = _uiElements.FirstOrDefault(e => e.GetType() == typeof(T));
            if (uiElement != null)
            {
                uiElement.Show();
                _currentUIElement = uiElement;
                _currentUIElement.OnElementHide += HideUIElement;
            }
        }

        public void HideUIElement()
        {
            _currentUIElement.OnElementHide -= HideUIElement;
            _currentUIElement.Hide();
            _currentUIElement = null;
        }

        /*public T GetUIElement<T>() where T : UIElement
        {
            var type = typeof(T);
            return (T) _uiElements[type];
        }*/
    }
}