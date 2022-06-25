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
    public class GameStart : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;
        
        private UIController _currentUIController;
        
        private void Start()
        {
            var sceneManager = new SceneManagerStart();

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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            var saveInteractor = Game.GetInteractor<SaveInteractor>();
            foreach (var interactor in Game.GetInteractors.Values)
            {
                if (interactor is ISavableInteractor savableInteractor)
                {
                    saveInteractor.AddEntity(savableInteractor.GetSavableEntity());
                }
            }

            saveInteractor.OnGameReloaded += RefreshUI;
            
            Game.OnGameInitialized -= OnGameInit;
        }

        private void RefreshUI()
        {
            _uiController.Refresh();
        }
    }
}