using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Moving
{
    [CreateAssetMenu(menuName = "AnimatorState/SprintState")]
    public class SprintingAnimatorState : BaseMovementAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            
            Player.ReusableData.MovementSpeedModifier = GroundedData.SprintData.SpeedModifier;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (Player.ReusableData.MovementInputWithNormalization == Vector2.zero)
            {
                Player.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, true);
                Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
                Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, true);
                return;
            }
            
            base.OnUpdate(characterState, animator, stateInfo);
        }

       
        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Player.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false);
            base.OnExit(characterState, animator, stateInfo);
        }
        
        protected override void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Sprint.canceled += OnSprintEnded;
            Player.InputAction.PlayerActions.Attack1.performed += OnSprintAttackCalled;
        }

        protected override void RemoveInputCallbacks()
        {
            Player.InputAction.PlayerActions.Attack1.performed -= OnSprintAttackCalled;
            Player.InputAction.PlayerActions.Sprint.canceled -= OnSprintEnded;
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, true);
        }
        
        private void OnSprintAttackCalled(InputAction.CallbackContext obj)
        {
            if(Player.ReusableData.MovementInputWithNormalization != Vector2.zero)
                Player.Animator.SetBool(PlayerAnimationData.SprintAttackParameterHash, true);
        }

        private void OnSprintEnded(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false);
        }
        
    }
}