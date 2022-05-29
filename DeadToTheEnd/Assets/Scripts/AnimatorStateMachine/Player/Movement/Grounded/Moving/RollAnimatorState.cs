using AnimatorStateMachine.Movement;
using AnimatorStateMachine.Movement.Grounded.Moving;
using StateMachine.WarriorEnemy;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace AnimatorStateMachine.Player.Movement.Grounded.Moving
{
    [CreateAssetMenu(menuName = "AnimatorState/RollState")]
    public class RollAnimatorState : BaseMovementAnimatorState
    {
        [SerializeField] private float _endRollTime = 0.9f;
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);

            MainPlayer.ReusableData.IsPerformingAction = true;
            MainPlayer.ReusableData.MovementSpeedModifier = MainPlayer.PlayerData.GroundData.RollData.SpeedModifier;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RotateTowardsTargetRotation();
            if (_endRollTime < stateInfo.normalizedTime)
            {
                ResetVelocity();                
                return;
            }
            
            Roll();
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            MainPlayer.ReusableData.MovementSpeedModifier = GroundedData.PlayerRunData.StrafeSpeedModifier;
            MainPlayer.ReusableData.IsPerformingAction = false;

            ResetVelocity();
            MainPlayer.Animator.SetBool(PlayerAnimationData.RollParameterHash, false);
        }
        
        private void Roll()
        {
            Vector3 dashDirection = MainPlayer.transform.forward;

            dashDirection.y = 0f;

            UpdateTargetRotation(dashDirection, false);

            if (MainPlayer.ReusableData.MovementInputWithNormalization != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection().normalized);

                dashDirection = GetTargetRotationDirection(MainPlayer.ReusableData.CurrentTargetRotation.y);
            }

            MainPlayer.Rigidbody.velocity = dashDirection * GetMaxMovementSpeed(false);
        }

        protected override void AddInputCallbacks()
        {
        }

        protected override void RemoveInputCallbacks()
        {
        }
    }
}