using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Enemies;
using GameCore;
using GameCore.Player;
using LootSystem;
using UI.Inventory.ItemContainers.Core;
using UI.Menu.Core;
using UI.Tip;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextTipUI _textTipUI;

        private UIElement _currentUIElement;
        private Dictionary<Type, IUIElement> _createdUIElements;

        protected List<ItemContainerUI> ItemContainersUi;
        protected List<UIElement> UIElements;
        protected MainPlayer MainPlayer;
        protected IRefreshable[] RefreshableElements;
        protected GameInput GameInput;

        public void SetInputAction(GameInput gameInput)
        {
            GameInput = gameInput;
        }
        public virtual void SendMessageOnCreate(InteractorsBase interactorsBase)
        {
            MainPlayer = interactorsBase.GetInteractor<PlayerInteractor>().MainPlayer;
            MainPlayer.GetComponent<InteractableChecker>().OnInteractableRequest += ActivateInteractableUI;
            MainPlayer.GetComponent<InteractableChecker>().OnInteractableHide += DeactivateInteractableUI;
        }

        public virtual void SendMessageOnInitialize(InteractorsBase interactorsBase)
        {
            Init(interactorsBase);
        }

        public virtual void SendMessageOnStart(InteractorsBase interactorsBase)
        {
        }

        protected virtual void Init(InteractorsBase interactorsBase)
        {
            gameObject.SetActive(true);

            RefreshableElements = GetComponentsInChildren<IRefreshable>();
            ItemContainersUi = GetComponentsInChildren<ItemContainerUI>().ToList();
            foreach (var itemContainerUI in ItemContainersUi)
            {
                itemContainerUI.Init(interactorsBase);
            }

            UIElements = GetComponentsInChildren<UIElement>().ToList();
            foreach (var uiElement in UIElements)
            {
                uiElement.OnElementShow += element => _currentUIElement = (UIElement) element;
                uiElement.Hide();
                uiElement.OnCreate(interactorsBase);
            }

            _currentUIElement = null;
        }

        protected void SwitchUIElement<T>() where T : IUIElement
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

        private void ShowUIElement<T>() where T : IUIElement
        {
            var uiElement = UIElements.FirstOrDefault(e => e.GetType() == typeof(T));
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

        public void Refresh()
        {
            foreach (var refreshable in RefreshableElements)
            {
                refreshable.Refresh();
            }
        }

        protected virtual void ActivateInteractableUI(IInteractable obj)
        {
            _textTipUI.SetText(obj.TextOfInteraction());
            _textTipUI.gameObject.SetActive(true);
        }

        protected virtual void DeactivateInteractableUI()
        {
            _textTipUI.gameObject.SetActive(false);
            _textTipUI.SetText("");
        }

        protected virtual void OnDisable()
        {
        }

        protected void TryToSwitchToMenu(InputAction.CallbackContext obj)
        {
            if (_currentUIElement == null)
            {
                SwitchUIElement<MainMenuSwitcher>();
            }
            else
            {
                HideUIElement();
            }
        }
    }
}