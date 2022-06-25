using UnityEngine;

namespace StateMachine.Game
{
    public class UIState: IState
    {
        public void Enter()
        {
            ActivateCursor();
        }

        private void ActivateCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        private void DeactivateCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public void Exit()
        {
            DeactivateCursor();
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void TriggerOnStateAnimationEnterEvent()
        {
            
        }

        public void TriggerOnStateAnimationExitEvent()
        {
            
        }

        public void TriggerOnStateAnimationHandleEvent()
        {
            
        }

        public void HandleInput()
        {
            
        }
    }
}