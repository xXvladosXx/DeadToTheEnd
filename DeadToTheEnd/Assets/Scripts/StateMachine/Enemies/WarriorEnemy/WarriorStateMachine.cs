using System;
using System.Collections.Generic;
using Entities;
using Entities.Core;
using Entities.Enemies;
using StateMachine.Core;
using StateMachine.Enemies.GoblinEnemy.States.Movement;
using StateMachine.WarriorEnemy.Components;
using StateMachine.WarriorEnemy.States.Combat;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.WarriorEnemy
{
    public class WarriorStateMachine : EntityStateMachine
    {
        public IdleWarriorEnemyState IdleWarriorEnemyState { get; }
        public FollowWarriorEnemyState FollowWarriorEnemyState { get; }
        public PatrolWarriorEnemyState PatrolWarriorEnemyState { get; }
        public LightAttackWarriorWarriorEnemyState LightAttackWarriorWarriorEnemyState { get; }
        public DashFirstAttackWarriorWarriorEnemyState DashFirstAttackWarriorWarriorEnemyState { get; }
        public DashSecondAttackWarriorWarriorEnemy DashSecondAttackWarriorWarriorEnemy { get; }
        public RotateTowardsTargetEnemyState RotateTowardsEnemyState { get; }
        public ForwardMoveWarriorEnemyState BaseForwardMoveEnemyState { get; }
        public StrafeMoveWarriorEnemyState StrafeMoveWarriorEnemyState { get; }
        public BaseRollEnemyState BaseRollBackWarriorEnemyState { get; }
        public ComboFirstWarriorWarriorEnemyState ComboFirstWarriorWarriorEnemyState { get; }
        public ComboSecondWarriorWarriorEnemyState ComboSecondWarriorWarriorEnemyState { get; }
        
        public WarriorStateMachine(Entities.Enemies.WarriorEnemy aliveEntity)
        {
            AliveEntity = aliveEntity;
            
            IdleWarriorEnemyState = new IdleWarriorEnemyState(this);
            FollowWarriorEnemyState = new FollowWarriorEnemyState(this);
            PatrolWarriorEnemyState = new PatrolWarriorEnemyState(this);
            LightAttackWarriorWarriorEnemyState = new LightAttackWarriorWarriorEnemyState(this);
            DashFirstAttackWarriorWarriorEnemyState = new DashFirstAttackWarriorWarriorEnemyState(this);
            RotateTowardsEnemyState = new RotateTowardsTargetEnemyState(this);
            BaseForwardMoveEnemyState = new ForwardMoveWarriorEnemyState(this);
            StrafeMoveWarriorEnemyState = new StrafeMoveWarriorEnemyState(this);
            BaseRollBackWarriorEnemyState = new BaseRollEnemyState(this);
            ComboFirstWarriorWarriorEnemyState = new ComboFirstWarriorWarriorEnemyState(this);
            DashSecondAttackWarriorWarriorEnemy = new DashSecondAttackWarriorWarriorEnemy(this);
            ComboSecondWarriorWarriorEnemyState = new ComboSecondWarriorWarriorEnemyState(this);
        }

        public override IState StartState() => FollowWarriorEnemyState;
        
    }
}