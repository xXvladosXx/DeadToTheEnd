using System;
using GameCore;
using UnityEngine;

namespace UI
{
    public abstract class UIElement : MonoBehaviour, IUIElement
    {
        public event Action OnElementHide;
        public event Action<IUIElement> OnElementShow;
        public event Action OnCursorShow;
        public event Action OnCursorHide;

        public bool IsActive { get; }
        public string Name { get; }
        public GameObject GameObject { get; }

        private bool _isActive;

        public void Show()
        {
            gameObject.SetActive(true);
            OnElementShow?.Invoke(this);
            OnCursorShow?.Invoke();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnElementHide?.Invoke();
            OnCursorHide?.Invoke();
        }

        public abstract void OnCreate(InteractorsBase interactorsBase);
    }
}