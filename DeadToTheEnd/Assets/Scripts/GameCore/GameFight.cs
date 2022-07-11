using System;
using GameCore.Player;
using GameCore.Save;
using GameCore.SceneSystem;
using UI;
using UI.Controllers;
using UnityEngine;
using Utilities;

namespace GameCore
{
    [RequireComponent(typeof(GameFight))]
    public class GameFight : MonoBehaviour
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
            var sceneManager = new SceneManagerFight();

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

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            var saveInteractor = Game.GetInteractor<SaveInteractor>();
            foreach (var interactor in Game.GetInteractors.Values)
            {
                if (interactor is ISavableInteractor savableInteractor)
                {
                    saveInteractor.AddEntity(savableInteractor.GetSavableEntity());
                }
            }

            saveInteractor.Load();
            RefreshUI();

            Game.OnGameInitialized -= OnGameInit;
            Destroy(_secondCamera.gameObject);
            
            var playerInteractor = Game.GetInteractor<PlayerInteractor>();
            playerInteractor.MainPlayer.InputAction.enabled = true;
        }

        private void RefreshUI()
        {
            if(_uiController == null) return;
            _uiController.Refresh();
        }
    }
}