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
                TargetLocked();
           
            MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDefenseCalled;
            MainPlayer.InputAction.PlayerActions.Roll.performed += OnRollCalled;
        }

        private void OnRollCalled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.RollParameterHash, true);
        }

        private void OnDefenseCalled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, true);
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDefenseCalled;
            MainPlayer.InputAction.PlayerActions.Roll.performed -= OnRollCalled;
        }
    }
}