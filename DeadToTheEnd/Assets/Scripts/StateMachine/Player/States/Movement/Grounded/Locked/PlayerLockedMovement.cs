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
            MainPlayer.ReusableData.IsMovingAfterStop = true;
            MainPlayer.ReusableData.MovementSpeedModifier = PlayerGroundData.PlayerRunData.StrafeSpeedModifier;
            MainPlayer.GetComponent<EnemyLockOn>().FoundTarget();
                
            base.Enter();
            StartAnimation(PlayerAnimationData.LockedParameterHash);
            
            if(MainPlayer.ReusableData.ShouldBlock)
                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerDefenseState);
        }

        public override void Update()
        {
            base.Update(); 
           
            MainPlayer.Animator.SetFloat(PlayerAnimationData.VerticalParameterHash, MainPlayer.ReusableData.MovementInputWithoutNormalization.y, 0.2f, Time.deltaTime);
            MainPlayer.Animator.SetFloat(PlayerAnimationData.HorizontalParameterHash, MainPlayer.ReusableData.MovementInputWithoutNormalization.x, 0.2f, Time.deltaTime);
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
        private void OnRollPerformed(InputAction.CallbackContext obj)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerRollState);
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