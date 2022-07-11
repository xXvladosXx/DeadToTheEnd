using System;
using GameCore.Player;
using GameCore.Save;
using UI.Controllers;
using UnityEngine;

namespace GameCore.SceneSystem
{
    public class GameMenu: MonoBehaviour
    {
        [SerializeField] private UIController _uiController;
        
        private UIController _currentUIController;
        
        private void Start()
        {
            var sceneManager = new SceneManagerMenu();

            Game.Run(sceneManager, _uiController);

            Game.OnGameInitialized += OnGameInit;
        }

        private void OnGameInit()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            Game.OnGameInitialized -= OnGameInit;
        }
    }
}