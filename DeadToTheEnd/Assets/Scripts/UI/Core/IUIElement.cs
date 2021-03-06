using System;
using UnityEngine;

namespace UI
{
    public interface IUIElement : ICaptureEvents
    {
        event Action OnElementHide;
        event Action<IUIElement> OnElementShow;

        bool IsActive { get; }
        string Name { get; }
        GameObject GameObject { get; }

        void Show();
        void Hide();
    }
}