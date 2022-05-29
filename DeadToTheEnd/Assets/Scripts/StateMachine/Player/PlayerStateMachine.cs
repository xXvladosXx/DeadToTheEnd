using Data.States;
using Entities;
using StateMachine.Player.States.Movement.Grounded;
using StateMachine.Player.States.Movement.Grounded.Combat;
using StateMachine.Player.States.Movement.Grounded.Defense;
using StateMachine.Player.States.Movement.Grounded.Locked;
using StateMachine.Player.States.Movement.Grounded.Moving;
using StateMachine.Player.States.Movement.Grounded.Stopping;
using UnityEngine;

namespace StateMachine
{
    public class PlayerStateMachine : StateMachine
    {
        public MainPlayer MainPlayer { get; }

        public PlayerIdleState PlayerIdleState { get; }
        public PlayerDashState PlayerDashState { get; }
        public PlayerLockedMovement PlayerLockedMovementState { get; }

        public PlayerWalkingState PlayerWalkingState { get; }
        public PlayerRunningState PlayerRunningState { get; }
        public PlayerSprintingState PlayerSprintingState { get; }
        public PlayerLightStoppingState LightStoppingState { get; }
        public PlayerMediumStoppingState MediumStoppingState { get; }
        public PlayerDefenseState PlayerDefenseState { get; }
        public PlayerDefenseImpactState PlayerDefenseImpactState { get; }
        public PlayerHardStoppingState HardStoppingState { get; }
        public PlayerRollState PlayerRollState { get; }
        public PlayerAttackLockedState PlayerAttackLockedState { get; set; }
        public PlayerComboLockedAttackState PlayerComboLockedAttackState { get; set; }
        public PlayerComboAttackState PlayerComboAttackState { get; set; }
        public PlayerAttackState PlayerAttackState { get; set; }


        public PlayerStateMachine(MainPlayer player, GameObject gameObject)
        {
            MainPlayer = player;
            
            PlayerIdleState = new PlayerIdleState(this);
            PlayerDashState = new PlayerDashState(this);

            PlayerWalkingState = new PlayerWalkingState(this);
            PlayerRunningState = new PlayerRunningState(this);
            PlayerSprintingState = new PlayerSprintingState(this);

            PlayerLockedMovementState = new PlayerLockedMovement(this);
            
            PlayerAttackLockedState = new PlayerAttackLockedState(this);
            PlayerComboLockedAttackState = new PlayerComboLockedAttackState(this);
            PlayerAttackState = new PlayerAttackState(this);
            PlayerComboAttackState = new PlayerComboAttackState(this);
            
            PlayerDefenseState = new PlayerDefenseState(this);
            PlayerRollState = new PlayerRollState(this);
            
            LightStoppingState = new PlayerLightStoppingState(this);
            MediumStoppingState = new PlayerMediumStoppingState(this);
            HardStoppingState = new PlayerHardStoppingState(this);
            PlayerDefenseImpactState = new PlayerDefenseImpactState(this);
            //PlayerLockedStoppingState = new PlayerLockedStoppingState(this);
        }

        public override IState StartState() => PlayerIdleState;
    }
}