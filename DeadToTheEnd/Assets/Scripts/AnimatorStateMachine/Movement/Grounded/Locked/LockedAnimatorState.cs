using AnimatorStateMachine.Movement.Grounded.Moving;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Locked
{
    
    [CreateAssetMenu(menuName = "AnimatorState/LockState")]

    public class LockedAnimatorState : RunningAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnEnter(characterState, animator, stateInfo, playerInputActions);
            Player.ReusableData.MovementSpeedModifier = GroundedData.RunData.SpeedModifier;
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnExit(characterState, animator, stateInfo, playerInputActions);
            
            ResetVelocity();
            Player.Animator.SetBool(PlayerAnimationData.DashParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false); 
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
        }

        protected override void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
            Player.InputAction.PlayerActions.Attack1.performed += OnAttackCalled;
            Player.InputAction.PlayerActions.Dash.performed += OnDefenseCalled;
        }

        private void OnDefenseCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, true);
        }

        private void OnAttackCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(Player.PlayerAnimationData.MovingParameterHash, true);
        }

        protected override void RemoveInputCallbacks()
        {
            Player.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
            Player.InputAction.PlayerActions.Attack1.performed -= OnAttackCalled;
            Player.InputAction.PlayerActions.Dash.performed -= OnDefenseCalled;
        }
    }
}