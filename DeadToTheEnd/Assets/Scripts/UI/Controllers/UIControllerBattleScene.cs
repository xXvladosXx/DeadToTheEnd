using System;
using CameraManage;
using Entities.Enemies;
using GameCore;
using GameCore.GameSystem;
using LootSystem;
using UI.HUD;
using UnityEngine;

namespace UI.Controllers
{
    public class UIControllerBattleScene : UIController
    {
        [SerializeField] private EnemyHUDUI _enemyHudui;
        
        private GameStateInteractor _gameStateInteractor;

        private EnemyLockOn _enemyLock;
        public override void SendMessageOnInitialize(InteractorsBase interactorsBase)
        {
            base.SendMessageOnInitialize(interactorsBase);
            _gameStateInteractor = interactorsBase.GetInteractor<GameStateInteractor>();
            _enemyLock = MainPlayer.GetComponent<EnemyLockOn>();
            
            _enemyLock.OnPlayerLocked += _enemyHudui.InitEnemyData;
            _enemyLock.OnPlayerUnlocked += DeactivateEnemyHUDUI;
            
            foreach (var uiElement in UIElements)
            {
                uiElement.OnCursorShow += ElementOnCursorShow();
                uiElement.OnCursorHide += ElementOnCursorHide();
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _enemyLock.OnPlayerLocked -= _enemyHudui.InitEnemyData;
            _enemyLock.OnPlayerUnlocked -= DeactivateEnemyHUDUI;
            
            foreach (var uiElement in UIElements)
            {
                uiElement.OnCursorShow -= ElementOnCursorShow();
                uiElement.OnCursorHide -= ElementOnCursorHide();
            }
        }

        protected override void ActivateInteractableUI(IInteractable obj)
        {
            base.ActivateInteractableUI(obj);
            
            switch (obj)
            {
                case Enemy enemy:
                    if (MainPlayer.PlayerStateReusable.Target != null)
                    {
                        break;
                    }

                    _enemyHudui.InitEnemyData(enemy);
                    break;
            }
        }

        protected override void DeactivateInteractableUI()
        {
            base.DeactivateInteractableUI();
            
            if(MainPlayer.PlayerStateReusable.Target != null) return;
            DeactivateEnemyHUDUI();
        }
        
        private void DeactivateEnemyHUDUI()
        {
            _enemyHudui.gameObject.SetActive(false);
        }

        private Action ElementOnCursorHide() => () => _gameStateInteractor.ChangeState(GameStateInteractor.GameStates.Battle);

        private Action ElementOnCursorShow() => () => _gameStateInteractor.ChangeState(GameStateInteractor.GameStates.UI);
    }
}