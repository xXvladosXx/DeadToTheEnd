using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Moving
{
    [CreateAssetMenu(menuName = "AnimatorState/DashState")]
    public class DashAnimatorState: BaseMovementAnimatorState
    {
        [SerializeField] private float _startTimeToDash;
        [SerializeField] private float _endTimeToDash;

        private bool _shouldKeepRotating;
        private bool _shouldKeepSprinting;
        private float _f;
        
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            _shouldKeepRotating = Player.ReusableData.MovementInputWithNormalization != Vector2.zero;
            _shouldKeepSprinting = false;
            Player.ReusableData.IsMovingAfterStop = false;
            _f = 0;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            _f += Time.deltaTime;
            
            if (stateInfo.normalizedTime < _endTimeToDash && stateInfo.normalizedTime > _startTimeToDash)
            {
                Player.ReusableData.MovementSpeedModifier = GroundedData.DashData.SpeedModifier-(_f*3);

                Dash();
                if (!_shouldKeepRotating)
                {
                    return;
                }

                RotateTowardsTargetRotation();
                return;
            }
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Player.ReusableData.MovementSpeedModifier = GroundedData.SprintData.SpeedModifier;
            if(!_shouldKeepRotating)
                Player.Animator.SetBool(PlayerAnimationData.SprintParameterHash, false);
            
            SetBaseRotationData();
        }
        
        private void Dash()
        {
            Vector3 dashDirection = Player.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (Player.ReusableData.MovementInputWithNormalization != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection().normalized);

                dashDirection = GetTargetRotationDirection(Player.ReusableData.CurrentTargetRotation.y);
            }

            Player.Rigidbody.velocity = dashDirection * GetMaxMovementSpeed(false);
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            Player.InputAction.PlayerActions.Sprint.performed += OnMovementPerformed;

        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _shouldKeepSprinting = true;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            Player.InputAction.PlayerActions.Sprint.performed -= OnMovementPerformed;
        }
    }
}