using UnityEngine;

namespace Utilities
{
    public class GameInput : MonoBehaviour
    {
        public GameInputActions InputAction { get; private set; }
        public GameInputActions.BarActions BarActions { get; private set; }

        private void Awake()
        {
            InputAction = new GameInputActions();
            
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

    }
}