using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Stopping
{
    [CreateAssetMenu(menuName = "AnimatorState/LockedStopState")]
    public class LockedStoppingAnimatorState : StoppingAnimatorState
    {
        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(Player.ReusableData.IsTargetLocked)
                TargetLocked();
           
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            Player.InputAction.PlayerActions.Dash.performed += OnDefenseCalled;
            Player.InputAction.PlayerActions.Roll.performed += OnRollCalled;
        }

        private void OnRollCalled(InputAction.CallbackContext obj)
        {
            Player.ReusableData.IsTargetLocked = false;
            Player.Animator.SetBool(PlayerAnimationData.RollParameterHash, true);
        }

        private void OnDefenseCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, true);
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            Player.InputAction.PlayerActions.Dash.performed -= OnDefenseCalled;
            Player.InputAction.PlayerActions.Roll.performed -= OnRollCalled;
        }
    }
}