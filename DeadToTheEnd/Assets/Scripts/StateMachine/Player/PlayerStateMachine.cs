using Data.States;
using Entities;
using StateMachine.Core;
using StateMachine.Enemies;
using StateMachine.Player.States.Movement.Grounded;
using StateMachine.Player.States.Movement.Grounded.Combat;
using StateMachine.Player.States.Movement.Grounded.Defense;
using StateMachine.Player.States.Movement.Grounded.Locked;
using StateMachine.Player.States.Movement.Grounded.Locked.Hit;
using StateMachine.Player.States.Movement.Grounded.Moving;
using StateMachine.Player.States.Movement.Grounded.Stopping;
using UnityEngine;

namespace StateMachine
{
    public class PlayerStateMachine : EntityStateMachine
    {
        public MainPlayer MainPlayer { get; }

        public PlayerIdleState PlayerIdleState { get; }
        public PlayerDashState PlayerDashState { get; }
        public PlayerLockedMovement PlayerLockedMovementState { get; }

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
        public PlayerKnockHitState PlayerKnockHitState { get; }
        
        public PlayerActiveSkillCastState PlayerActiveSkillCastState { get; }

        public PlayerDieState PlayerDieState { get; } 

        public PlayerStateMachine(MainPlayer player)
        {
            MainPlayer = player;
            
            PlayerIdleState = new PlayerIdleState(this);
            PlayerDashState = new PlayerDashState(this);

            PlayerRunningState = new PlayerRunningState(this);
            PlayerSprintingState = new PlayerSprintingState(this);

            PlayerLockedMovementState = new PlayerLockedMovement(this);
            
            PlayerKnockHitState = new PlayerKnockHitState(this);
            
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

            PlayerActiveSkillCastState = new PlayerActiveSkillCastState(this);

            PlayerDieState = new PlayerDieState(this);
        }

        public override IState StartState() => MediumStoppingState;
    }
}