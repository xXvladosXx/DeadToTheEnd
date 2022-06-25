using GameCore.Player;
using GameCore.Save;
using GameCore.SceneSystem;
using UI;
using UnityEngine;

namespace GameCore
{
    public class GameFight : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;

        private UIController _currentUIController;

        private void Start()
        {
            var sceneManager = new SceneManagerFight();

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

            saveInteractor.OnGameReloaded += RefreshUI;

            Game.OnGameInitialized -= OnGameInit;

            var playerInteractor = Game.GetInteractor<PlayerInteractor>();
            playerInteractor.MainPlayer.InputAction.enabled = true;
        }

        private void RefreshUI()
        {
            _uiController.Refresh();
        }
    }
}