using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Moving
{
    [CreateAssetMenu(menuName = "AnimatorState/SprintState")]
    public class SprintingAnimatorState : BaseMovementAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnEnter(characterState, animator, stateInfo, playerInputActions);
            
            Player.ReusableData.MovementSpeedModifier = GroundedData.SprintData.SpeedModifier;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            if (Player.ReusableData.MovementInput == Vector2.zero)
            {
                Player.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, true);
                return;
            }
            
            Player.ReusableData.MovementSpeedModifier = GroundedData.SprintData.SpeedModifier;
            base.OnUpdate(characterState, animator, stateInfo, playerInputActions);
        }

       
        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            Player.ReusableData.MovementSpeedModifier = GroundedData.SprintData.SpeedModifier;

            base.OnExit(characterState, animator, stateInfo, playerInputActions);
        }
        
        protected override void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Sprint.canceled += OnSprintEnded;
            Player.InputAction.PlayerActions.Attack1.performed += OnSprintAttackCalled;
        }

        protected override void RemoveInputCallbacks()
        {
            Player.InputAction.PlayerActions.Attack1.performed -= OnSprintAttackCalled;
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, true);
            base.OnMovementCanceled(obj);
        }
        
        private void OnSprintAttackCalled(InputAction.CallbackContext obj)
        {
            if(Player.ReusableData.MovementInput != Vector2.zero)
                Player.Animator.SetBool(PlayerAnimationData.SprintAttackParameterHash, true);
        }

        private void OnSprintEnded(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false);
        }
        
    }
}