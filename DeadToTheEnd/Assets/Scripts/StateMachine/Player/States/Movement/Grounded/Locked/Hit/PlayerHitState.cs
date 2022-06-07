using Data.Combat;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Locked.Hit
{
    public abstract class PlayerHitState : PlayerLockedState
    {
        public PlayerHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            MainPlayer.Animator.applyRootMotion = true;
        }

        public override void FixedUpdate()
        {
            UnmovableLocked();
        }

        protected override void OnDamageTaken(AttackData attackData)
        {
        }

        protected override void AddInputCallbacks()
        {
        }

        protected override void RemoveInputCallbacks()
        {
        }
        
        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            
            if (MainPlayer.PlayerStateReusable.LockedState)
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
                return;
            }
            
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }

        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }

        public override void Exit()
        {
            base.Exit();
            MainPlayer.Animator.applyRootMotion = false;
        }
    }
}