using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Combat.Attacks
{
    [CreateAssetMenu(menuName = "AnimatorState/AttackSprintState")]
    public class AttackSprintAnimatorState : BaseCombatAnimatorState
    {
        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            Player.InputAction.PlayerActions.Sprint.canceled += OnSprintCalled;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            Player.InputAction.PlayerActions.Sprint.canceled -= OnSprintCalled;
        }

        private void OnSprintCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(Player.PlayerAnimationData.SprintParameterHash, false);
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnExit(characterState, animator, stateInfo, playerInputActions);
            Player.Animator.SetBool(Player.PlayerAnimationData.SprintAttackParameterHash, false);
            Player.Animator.SetBool(Player.PlayerAnimationData.ComboParameterHash, false);
            Player.Animator.SetBool(Player.PlayerAnimationData.Attack1ParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
        }

    }
}