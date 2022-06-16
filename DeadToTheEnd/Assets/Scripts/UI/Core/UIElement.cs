using System;
using UnityEngine;

namespace UI
{
    public abstract class UIElement: MonoBehaviour, IUIElement
    {
        public event Action OnElementHide;
        public event Action<IUIElement> OnElementShow;
        
        public bool IsActive { get; }
        public string Name { get; }
        public GameObject GameObject { get; }

        private bool _isActive;
        public void Show()
        {
            gameObject.SetActive(true);
            OnElementShow?.Invoke(this);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnElementHide?.Invoke();   
        }

        public abstract void OnCreate();
        public abstract void OnInitialize();
        public abstract void OnStart();
    }
}