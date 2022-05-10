using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Stopping
{
    [CreateAssetMenu(menuName = "AnimatorState/LockedStopState")]
    public class LockedStoppingAnimatorState : StoppingAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnEnter(characterState, animator, stateInfo, playerInputActions);
            ResetVelocity();
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            if (Player.ReusableData.MovementInput != Vector2.zero)
            {
                Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, true);
            }
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnExit(characterState, animator, stateInfo, playerInputActions);
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            Player.InputAction.PlayerActions.Dash.performed += OnDefenseCalled;
        }

        private void OnDefenseCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, true);
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            Player.InputAction.PlayerActions.Dash.performed -= OnDefenseCalled;
        }
    }
}