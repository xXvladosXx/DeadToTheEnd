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
            Player.ReusableData.MovementSpeedModifier = GroundedData.PlayerRunData.StrafeSpeedModifier;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Player.ReusableData.IsTargetLocked = true;

            if (Player.ReusableData.MovementInputWithNormalization == Vector2.zero && !Player.ReusableData.IsStopped)
            {
                Player.ReusableData.IsStopped = true;
                Player.ReusableData.IsMovingAfterStop = true;
                ResetVelocity();
            }else if (Player.ReusableData.MovementInputWithNormalization != Vector2.zero)
            {
                Player.ReusableData.IsStopped = false;
            }
            
            base.OnUpdate(characterState, animator, stateInfo);

            Player.Animator.SetFloat(PlayerAnimationData.VerticalParameterHash, Player.ReusableData.MovementInputWithoutNormalization.y, 0.2f, Time.deltaTime);
            Player.Animator.SetFloat(PlayerAnimationData.HorizontalParameterHash, Player.ReusableData.MovementInputWithoutNormalization.x, 0.2f, Time.deltaTime);
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            
            ResetVelocity();
            Player.Animator.SetBool(PlayerAnimationData.DashParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false); 
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
            
            Player.ReusableData.IsTargetLocked = false;
        }

        protected override void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
            Player.InputAction.PlayerActions.Attack1.performed += OnAttackCalled;
            Player.InputAction.PlayerActions.Dash.performed += OnDefenseCalled;
            Player.InputAction.PlayerActions.Roll.performed += OnRollCalled;
        }

        private void OnRollCalled(InputAction.CallbackContext obj)
        {
            if(Player.ReusableData.IsPerformingAction) return;
            
            Player.ReusableData.IsTargetLocked = false;
            Player.Animator.SetBool(PlayerAnimationData.RollParameterHash, true);
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
            Player.InputAction.PlayerActions.Roll.performed -= OnRollCalled;
        }
    }
}