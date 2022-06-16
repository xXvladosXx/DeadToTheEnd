using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputAction InputAction { get; private set; }
        public PlayerInputAction.PlayerActions PlayerActions { get; private set; }
        public PlayerInputAction.BarActions BarActions { get; private set; }

        private void Awake()
        {
            InputAction = new PlayerInputAction();
            
            PlayerActions = InputAction.Player;
            BarActions = InputAction.Bar;
        }

        private void OnEnable()
        {
            InputAction.Enable();
        }

        private void OnDisable()
        {
            InputAction.Disable();
         }

        public void DisableActionFor(InputAction inputAction, float seconds)
        {
            StartCoroutine(DisableAction(inputAction, seconds));
        }

        private IEnumerator DisableAction(InputAction inputAction, float seconds)
        {
            inputAction.Disable();
            yield return new WaitForSeconds(seconds);
            inputAction.Enable();
        }
    }
}