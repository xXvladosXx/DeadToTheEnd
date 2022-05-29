using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Stopping
{
    public class StoppingAnimatorState : BaseMovementAnimatorState
    {
        [SerializeField] private float _timeToStopDecelerating;
        private bool _wasReset;

        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            MainPlayer.ReusableData.MovementSpeedModifier = 0f;
            MainPlayer.ReusableData.IsMovingAfterStop = true;
            SetBaseCameraRecenteringData();
            MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
            _wasReset = false;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RotateTowardsTargetRotation();
            DecelerateHorizontally();
            if (MainPlayer.ReusableData.MovementInputWithNormalization != Vector2.zero)
            {
                MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, true);
            }
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            
            MainPlayer.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, false);
            MainPlayer.Animator.SetBool(PlayerAnimationData.DashParameterHash, false);
            MainPlayer.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, false);
            MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);

            ResetVelocity();
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttack1Called;
            MainPlayer.InputAction.PlayerActions.Dash.performed += OnDashCalled;
        }

        private void OnDashCalled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.DashParameterHash, true);
        }

        private void OnAttack1Called(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttack1Called;
            MainPlayer.InputAction.PlayerActions.Dash.performed -= OnDashCalled;
        }
    }
}