﻿using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameCore;
using GameCore.GameSystem;
using GameCore.Player;
using GameCore.Save;
using LootSystem;
using UI.Tip;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextTipUI _textTipUI;
        
        protected MainPlayer MainPlayer;

        protected List<UIElement> UIElements;
        private UIElement _currentUIElement;

        private Dictionary<Type, IUIElement> _createdUIElements;
        
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
            
            UIElements = GetComponentsInChildren<UIElement>().ToList();
            foreach (var uiElement in UIElements)
            {
                uiElement.OnElementShow += element => _currentUIElement = (UIElement) element;
                uiElement.Hide();
                uiElement.OnCreate(interactorsBase);
            }
        }

      
        protected void SwitchUIElement<T>() where T : UIElement
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
            foreach (var refreshable in GetComponentsInChildren<IRefreshable>())
            {
                refreshable.Refresh();
            }
        }
        
        private void ActivateInteractableUI(IInteractable obj)
        {
            _textTipUI.SetText(obj.TextOfInteraction());
            _textTipUI.gameObject.SetActive(true);
        }
        private void DeactivateInteractableUI()
        {
            _textTipUI.gameObject.SetActive(false);
            _textTipUI.SetText("");
        }

        protected virtual void OnDisable()
        {
            
        }
    }
}