using CameraManage;
using StateMachine.Player.States.Movement.Grounded.Locked;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Defense
{
    public class PlayerDefenseState : PlayerLockedState
    {
        public PlayerDefenseState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            MainPlayer.AttackColliderActivator.enabled = false;
            MainPlayer.DefenseColliderActivator.ActivateCollider();
            
            StartAnimation(PlayerAnimationData.DefenseParameterHash);
            MainPlayer.PlayerStateReusable.ShouldBlock = true;
            MainPlayer.PlayerStateReusable.IsBlocking = true;
        }

        public override void Exit()
        {
            base.Exit();
            
            MainPlayer.DefenseColliderActivator.DeactivateCollider();
            MainPlayer.PlayerStateReusable.IsBlocking = false;
            MainPlayer.AttackColliderActivator.enabled = true;

            StopAnimation(PlayerAnimationData.DefenseParameterHash);
        }

        public override void FixedUpdate()
        {
            UnmovableLocked();
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Dash.canceled += OnBlockCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
            MainPlayer.Health.OnAttackApplied += OnDefenseImpact;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            MainPlayer.InputAction.PlayerActions.Dash.canceled -= OnBlockCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
            MainPlayer.Health.OnAttackApplied -= OnDefenseImpact;
        }
        
        private void OnDefenseImpact()
        {
            CinemachineShake.Instance.ShakeCamera(1, .5f);
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseImpactState);
        }
        private void OnBlockCanceled(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldBlock = false;
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
        }
    }
}