using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameCore;
using GameCore.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {

        [SerializeField] private Button _inventory;
        [SerializeField] private Button _shop;
        [SerializeField] private Button _skills;
        [SerializeField] private Button _quests;
        [SerializeField] private Button _fight;

        private List<UIElement> _uiElements;
        private UIElement _currentUIElement;
        private MainPlayer _mainPlayer;

        private Dictionary<Type, IUIElement> _createdUIElements;
        
        public void Init()
        {
            gameObject.SetActive(true);
            _createdUIElements = new Dictionary<Type, IUIElement>();
            foreach (Transform child in transform)
            {
                if(child.TryGetComponent(out IUIElement uiElement))
                {
                    _createdUIElements.Add(uiElement.GetType(), uiElement);
                }
            }
            
            _uiElements = GetComponentsInChildren<UIElement>().ToList();
            foreach (var uiElement in _uiElements)
            {
                uiElement.Hide();
            }

            _inventory.onClick.AddListener(SwitchUIElement<CharacterContainerUI>);
            _shop.onClick.AddListener(SwitchUIElement<ShopUI>);
            _skills.onClick.AddListener(SwitchUIElement<SkillsUI>);
            _quests.onClick.AddListener(SwitchUIElement<QuestsUI>);
            _fight.onClick.AddListener(SwitchUIElement<FightUI>);
        }

        private void CreateInputs()
        {
            ReadInventoryInput();
            ReadShopInput();
            ReadSkillsInput();
            ReadQuestsInput();
            ReadFightInput();
        }

        private void ReadFightInput()
        {
            _mainPlayer.InputAction.BarActions.Fight.performed += context => { SwitchUIElement<FightUI>(); };
        }

        private void ReadQuestsInput()
        {
            _mainPlayer.InputAction.BarActions.Quests.performed += context => { SwitchUIElement<QuestsUI>(); };
        }

        private void ReadSkillsInput()
        {
            _mainPlayer.InputAction.BarActions.Skills.performed += context => { SwitchUIElement<SkillsUI>(); };
        }

        private void ReadShopInput()
        {
            _mainPlayer.InputAction.BarActions.Shop.performed += context => { SwitchUIElement<ShopUI>(); };
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
        public void SendMessageOnCreate(InteractorsBase interactorsBase)
        {
            _mainPlayer = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer;
        }

        public void SendMessageOnInitialize(InteractorsBase interactorsBase)
        {
            Init();
            CreateInputs();
            
            foreach (var uiElement in _uiElements)
            {
                uiElement.OnCreate(interactorsBase);
            }
        }
        
        public void SendMessageOnStart(InteractorsBase interactorsBase)
        {
           
        }
        
        private T CreateAndShowElement<T>(UIElement prefab) where T : UIElement {
            _createdUIElements[typeof(T)] = prefab;
            prefab.Show();
            return (T) prefab;
        }
        
        public T GetUIElement<T>() where T : UIElement {
            
            var type = typeof(T);
            _createdUIElements.TryGetValue(type, out var uiElement);
            return (T) uiElement;
        }
        
        public void Clear() {
            if (_createdUIElements == null)
                return;

            var allCreatedUIElements = _createdUIElements.Values.ToArray();
            foreach (var uiElement in allCreatedUIElements)
                Destroy(uiElement.GameObject);

            _createdUIElements.Clear();
        }

       
    }
}