using System;
using UnityEngine;

namespace Utilities
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputAction InputAction { get; private set; }
        public PlayerInputAction.PlayerActions PlayerActions { get; private set; }

        private void Awake()
        {
            InputAction = new PlayerInputAction();
            
            PlayerActions = InputAction.Player;
        }

        private void OnEnable()
        {
            InputAction.Enable();
        }

        private void OnDisable()
        {
            InputAction.Disable();
         }
    }
}