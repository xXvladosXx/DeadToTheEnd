using System;
using System.Collections.Generic;
using System.Linq;
using GameCore;
using GameCore.Save;
using UnityEngine;

namespace UI.Menu.Core
{
    public class MainMenuSwitcher : UIElement
    {
        private static MainMenuSwitcher _instance;
        [SerializeField] private Menu _startingMenu;
        [SerializeField] private Menu[] _menus;
        
        private Menu _currentMenu;
        private readonly Stack<Menu> _history = new Stack<Menu>();

        private void Awake()
        {
            _instance = this;
        }

        public override void OnCreate(InteractorsBase interactorsBase)
        {
            var saveInteractor = interactorsBase.GetInteractor<SaveInteractor>();
            
            foreach (var menu in _menus)
            {
                menu.Initialize(saveInteractor);
                menu.HideMenu();
            }
            
            if (_startingMenu != null)
            {
                Show(_startingMenu, true);
            }
        }

      
        public static void Show<T>(bool remember = true) where  T : Menu
        {
            foreach (var menu in _instance._menus)
            {
                if (menu is T)
                {
                    if (_instance._currentMenu != null)
                    {
                        if (remember)
                        {
                            _instance._history.Push(_instance._currentMenu);
                        }
                        
                        _instance._currentMenu.HideMenu();
                    }
                    
                    menu.ShowMenu();
                    _instance._currentMenu = menu;
                }
            }
        }

        private static void Show(Menu menu, bool remember = true)
        {
            if (_instance._currentMenu != null)
            {
                if (remember)
                {
                    _instance._history.Push(_instance._currentMenu);
                }
                
                _instance._currentMenu.HideMenu();
            }
            
            if (remember && _instance._currentMenu == null)
            {
                _instance._history.Push(_instance._currentMenu);
            }
            menu.ShowMenu();
            _instance._currentMenu = menu;
        }

        public static void ShowLast()
        {
            if (_instance._history.Count != 0)
            {
                Show(_instance._history.Pop(), false);
            }
        }

        private void OnDisable()
        {
            if (_startingMenu != null)
            {
                Show(_startingMenu, true);
            }
        }
    }
}