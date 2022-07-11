using System;
using AudioSystem;
using Entities;
using GameCore.Player;
using GameCore.Save;
using GameCore.SceneSystem;
using UI;
using UI.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace GameCore
{
    [RequireComponent(typeof(GameInput))]
    public class GameStart : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;
        [SerializeField] private Camera _secondCamera;
        [SerializeField] private AudioClip _clip;

        private UIController _currentUIController;
        private GameInput _gameInput;


        private void Awake()
        {
            _gameInput = GetComponent<GameInput>();
        }
        private void Start()
        {
            var sceneManager = new SceneManagerStart();

            _uiController.SetInputAction(_gameInput);
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
            AudioManager.Instance.PlayMusicSound(_clip);
            
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
            saveInteractor.LoadFromLastSave(saveInteractor.GetLastSave);
            
            Destroy(_secondCamera.gameObject);
            Game.OnGameInitialized -= OnGameInit;
        }

        private void RefreshUI()
        {
            _uiController.Refresh();
        }
    }
}