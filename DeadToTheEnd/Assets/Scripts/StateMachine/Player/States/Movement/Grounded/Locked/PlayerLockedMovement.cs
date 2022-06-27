using System.Linq;
using CameraManage;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Locked
{
    public class PlayerLockedMovement : PlayerLockedState
    {
        public PlayerLockedMovement(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            MainPlayer.PlayerStateReusable.IsMovingAfterStop = true;
            MainPlayer.PlayerStateReusable.MovementSpeedModifier = PlayerGroundData.PlayerRunData.StrafeSpeedModifier;
                
            base.Enter();
            StartAnimation(PlayerAnimationData.LockedParameterHash);
            
            if(MainPlayer.PlayerStateReusable.ShouldBlock)
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
        }

        public override void Update()
        {
            base.Update(); 
           
            MainPlayer.Animator.SetFloat(PlayerAnimationData.VerticalParameterHash, MainPlayer.PlayerStateReusable.MovementInputWithoutNormalization.y, 0.2f, Time.deltaTime);
            MainPlayer.Animator.SetFloat(PlayerAnimationData.HorizontalParameterHash, MainPlayer.PlayerStateReusable.MovementInputWithoutNormalization.x, 0.2f, Time.deltaTime);
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

        /*protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
            StopAnimation(PlayerAnimationData.LockedParameterHash);
            MainPlayer.LongSwordActivator.DeactivateSword();
            
            foreach (var shortSwordActivator in MainPlayer.ShortSwordsActivator)
                shortSwordActivator.ActivateSword();
            
            MainPlayer.GetComponent<EnemyLockOn>().ResetTarget();
        }*/
    }
}