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

        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnEnter(characterState, animator, stateInfo, playerInputActions);
            Player.ReusableData.MovementSpeedModifier = 3f;
            Player.ReusableData.IsMovingAfterStop = true;
            SetBaseCameraRecenteringData();
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
            _wasReset = false;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            //Stop();
            RotateTowardsTargetRotation();
            if (stateInfo.normalizedTime > _timeToStopDecelerating)
            {
                ResetVelocity();
            }
            
            DecelerateHorizontally();
            if (Player.ReusableData.MovementInput != Vector2.zero)
            {
                Player.Animator.SetBool(PlayerAnimationData.MovingParameterHash, true);
            }
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnExit(characterState, animator, stateInfo, playerInputActions);
            
            Player.Animator.SetBool(PlayerAnimationData.StoppingParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.DashParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, false);
            Player.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, false);
            
            ResetVelocity();
        }
        
        private void Stop()
        {
            Vector3 dashDirection = Player.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            Player.Rigidbody.velocity = dashDirection * GroundedData.StopData.MediumDeceleration;
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            Player.InputAction.PlayerActions.Attack1.performed += OnAttack1Called;
            Player.InputAction.PlayerActions.Dash.performed += OnDashCalled;
        }

        private void OnDashCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.DashParameterHash, true);
        }

        private void OnAttack1Called(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            Player.InputAction.PlayerActions.Attack1.performed -= OnAttack1Called;
            Player.InputAction.PlayerActions.Dash.performed -= OnDashCalled;
        }
    }
}