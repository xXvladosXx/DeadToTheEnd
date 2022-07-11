using GameCore;
using GameCore.Save;
using UnityEngine;

namespace UI.Menu.Core
{
    public abstract class Menu : MonoBehaviour
    {
        protected string SaveFile = " ";
        protected SaveInteractor SaveInteractor;
        
        public virtual void Initialize(SaveInteractor saveInteractor)
        {
            SaveInteractor = saveInteractor;
        }
        public virtual void HideMenu() => gameObject.SetActive(false);
        public virtual void ShowMenu() => gameObject.SetActive(true);
    }
}