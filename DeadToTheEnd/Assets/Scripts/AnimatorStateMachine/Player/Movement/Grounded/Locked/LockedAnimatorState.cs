using AnimatorStateMachine.Movement.Grounded.Moving;
using StateMachine.WarriorEnemy;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Locked
{
    
    [CreateAssetMenu(menuName = "AnimatorState/LockState")]

    public class LockedAnimatorState : RunningAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            MainPlayer.ReusableData.MovementSpeedModifier = GroundedData.PlayerRunData.StrafeSpeedModifier;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            if (MainPlayer.ReusableData.MovementInputWithNormalization == Vector2.zero && !MainPlayer.ReusableData.IsStopped)
            {
                MainPlayer.ReusableData.IsStopped = true;
                MainPlayer.ReusableData.IsMovingAfterStop = true;
                ResetVelocity();
            }else if (MainPlayer.ReusableData.MovementInputWithNormalization != Vector2.zero)
            {
                MainPlayer.ReusableData.IsStopped = false;
            }
            
            base.OnUpdate(characterState, animator, stateInfo);

            MainPlayer.Animator.SetFloat(PlayerAnimationData.VerticalParameterHash, MainPlayer.ReusableData.MovementInputWithoutNormalization.y, 0.2f, Time.deltaTime);
            MainPlayer.Animator.SetFloat(PlayerAnimationData.HorizontalParameterHash, MainPlayer.ReusableData.MovementInputWithoutNormalization.x, 0.2f, Time.deltaTime);
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            
            ResetVelocity();
            MainPlayer.Animator.SetBool(PlayerAnimationData.DashParameterHash, false);
            MainPlayer.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false); 
            MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
            MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
            
        }

        protected override void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackCalled;
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDefenseCalled;
            MainPlayer.InputAction.PlayerActions.Roll.performed += OnRollCalled;
        }

        private void OnRollCalled(InputAction.CallbackContext obj)
        {
            if(MainPlayer.ReusableData.IsPerformingAction) return;
            
            MainPlayer.Animator.SetBool(PlayerAnimationData.RollParameterHash, true);
        }

        private void OnDefenseCalled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, true);
        }

        private void OnAttackCalled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(MainPlayer.PlayerAnimationData.MovingParameterHash, true);
        }

        protected override void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackCalled;
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDefenseCalled;
            MainPlayer.InputAction.PlayerActions.Roll.performed -= OnRollCalled;
        }
    }
}