using System;
using Entities;
using GameCore.Player;
using UnityEngine;

namespace GameCore.GameSystem
{
    public class GameStateInteractor : Interactor
    {
        private MainPlayer _mainPlayer;
        public override void OnCreate()
        {
            base.OnCreate();
            _mainPlayer = Game.GetInteractor<PlayerInteractor>().MainPlayer;
        }

        public void ChangeState(GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.UI:
                    ActivateCursor();
                    break;
                case GameStates.Battle:
                    DeactivateCursor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
        
        private void ActivateCursor()
        {
            _mainPlayer.InputAction.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void DeactivateCursor()
        {
            _mainPlayer.InputAction.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public enum GameStates
        {
            UI,
            Battle
        }
    }
}