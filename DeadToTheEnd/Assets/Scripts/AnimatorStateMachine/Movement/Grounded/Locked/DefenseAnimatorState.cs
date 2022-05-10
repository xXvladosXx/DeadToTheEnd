using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Locked
{
    [CreateAssetMenu(menuName = "AnimatorState/DefenseState")]
    public class DefenseAnimatorState : LockedAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnEnter(characterState, animator, stateInfo, playerInputActions);
            Player.ReusableData.MovementSpeedModifier = 0f;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            TargetLocked();
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnExit(characterState, animator, stateInfo, playerInputActions);
            Player.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, false);
        }

        protected override void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Dash.canceled += OnDefenseCanceled;
            Player.InputAction.PlayerActions.Attack1.performed += OnAttackCalled;
        }

        protected override void RemoveInputCallbacks()
        {
            Player.InputAction.PlayerActions.Dash.canceled -= OnDefenseCanceled;
            Player.InputAction.PlayerActions.Attack1.performed -= OnAttackCalled;
        }

        private void OnAttackCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }
        
        private void OnDefenseCanceled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, false);
        }
    }
}