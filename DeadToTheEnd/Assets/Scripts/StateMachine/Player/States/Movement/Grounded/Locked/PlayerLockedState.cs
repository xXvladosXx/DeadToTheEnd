using CameraManage;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Locked
{
    public class PlayerLockedState : PlayerGroundedState
    {
        public PlayerLockedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ResetVelocity();
            MainPlayer.ReusableData.MovementInputWithNormalization = Vector2.zero;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            TargetLocked();
        }

        protected void UnmovableLocked()
        {
            TargetLocked();
            Float();
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Roll.performed += OnRollPerformed;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Roll.performed -= OnRollPerformed;
        }

        protected void OnRollPerformed(InputAction.CallbackContext obj)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerRollState);
        }
        
        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
        }
        
        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerAttackLockedState);
        }
        protected override void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            base.OnMovementCanceled(obj);
            ResetVelocity();
            //PlayerStateMachine.ChangeState(PlayerStateMachine.LockedStopping);
        }
        
        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
            StopAnimation(PlayerAnimationData.LockedParameterHash);
            MainPlayer.LongSwordActivator.DeactivateSword();
            
            foreach (var shortSwordActivator in MainPlayer.ShortSwordsActivator)
                shortSwordActivator.ActivateSword();
            
            MainPlayer.GetComponent<EnemyLockOn>().ResetTarget();
        }

        private void TargetLocked()
        {
            Transform transform;
            (transform = MainPlayer.transform).LookAt(MainPlayer.ReusableData.Target);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

    }
}