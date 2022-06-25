using System;
using GameCore;
using GameCore.GameSystem;
using UnityEngine;

namespace UI
{
    public class UIControllerBattleScene : UIController
    {
        private GameStateInteractor _gameStateInteractor;

        public override void SendMessageOnInitialize(InteractorsBase interactorsBase)
        {
            base.SendMessageOnInitialize(interactorsBase);
            _gameStateInteractor = interactorsBase.GetInteractor<GameStateInteractor>();

            foreach (var uiElement in UIElements)
            {
                uiElement.OnCursorShow += ElementOnCursorShow();
                uiElement.OnCursorHide += ElementOnCursorHide();
            }
        }

        private Action ElementOnCursorHide() => () => _gameStateInteractor.ChangeState(GameStateInteractor.GameStates.Battle);

        private Action ElementOnCursorShow() => () => _gameStateInteractor.ChangeState(GameStateInteractor.GameStates.UI);

        protected override void OnDisable()
        {
            base.OnDisable();
            foreach (var uiElement in UIElements)
            {
                uiElement.OnCursorShow -= ElementOnCursorShow();
                uiElement.OnCursorHide -= ElementOnCursorHide();
            }
        }
    }
}