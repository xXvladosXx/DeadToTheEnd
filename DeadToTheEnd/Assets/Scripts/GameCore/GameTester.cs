using System;
using Entities;
using GameCore.Player;
using GameCore.Save;
using GameCore.SceneSystem;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameCore
{
    public class GameTester : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;
        
        private UIController _currentUIController;
        
        private void Start()
        {
            var sceneManager = new SceneManagerExample();

            Game.Run(sceneManager, _uiController);

            Game.OnGameInitialized += OnGameInit;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Game.GetInteractor<SaveInteractor>().Save();
            }
            
            if (Input.GetKeyDown(KeyCode.V))
            {
                Game.GetInteractor<SaveInteractor>().Load();
            }
        }

        private void OnGameInit()
        {
            Game.OnGameInitialized -= OnGameInit;
        }

    }
}