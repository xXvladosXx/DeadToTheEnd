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
            
            MainPlayer.ReusableData.MovementSpeedModifier = GroundedData.SprintData.SpeedModifier;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (MainPlayer.ReusableData.MovementInputWithNormalization == Vector2.zero)
            {
                MainPlayer.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, true);
                MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
                MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, true);
                return;
            }
            
            base.OnUpdate(characterState, animator, stateInfo);
        }

       
        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false);
            base.OnExit(characterState, animator, stateInfo);
        }
        
        protected override void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Sprint.canceled += OnSprintEnded;
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnSprintAttackCalled;
        }

        protected override void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnSprintAttackCalled;
            MainPlayer.InputAction.PlayerActions.Sprint.canceled -= OnSprintEnded;
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, true);
        }
        
        private void OnSprintAttackCalled(InputAction.CallbackContext obj)
        {
            if(MainPlayer.ReusableData.MovementInputWithNormalization != Vector2.zero)
                MainPlayer.Animator.SetBool(PlayerAnimationData.SprintAttackParameterHash, true);
        }

        private void OnSprintEnded(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false);
        }
        
    }
}