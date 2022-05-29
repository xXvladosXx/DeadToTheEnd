using System;
using System.Collections.Generic;
using Entities;
using StateMachine.WarriorEnemy.Components;
using StateMachine.WarriorEnemy.States.Combat;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.WarriorEnemy
{
    public class WarriorStateMachine : StateMachine
    {
        public Enemy WarriorEnemy { get; }
        
        public IdleWarriorEnemyState IdleWarriorEnemyState { get; }
        public FollowWarriorEnemyState FollowWarriorEnemyState { get; }
        public PatrolWarriorEnemyState PatrolWarriorEnemyState { get; }
        public LightAttackWarriorEnemyState LightAttackWarriorEnemyState { get; }
        public DashFirstAttackWarriorEnemyState DashFirstAttackWarriorEnemyState { get; }
        public DashSecondAttackWarriorEnemy DashSecondAttackWarriorEnemy { get; }
        public RotateTowardsTargetEnemyState RotateTowardsEnemyState { get; }
        public ForwardMoveWarriorEnemyState ForwardMoveWarriorEnemyState { get; }
        public StrafeMoveWarriorEnemyState StrafeMoveWarriorEnemyState { get; }
        public RollBackWarriorEnemyState RollBackWarriorEnemyState { get; }
        public ComboFirstWarriorEnemyState ComboFirstWarriorEnemyState { get; }
        public ComboSecondWarriorEnemyState ComboSecondWarriorEnemyState { get; }

        public Dictionary<Type, float> StatesCooldown { get; private set; }
        public Dictionary<Type, float> CurrentStatesCooldown { get; private set; }

        private AttackCooldownTimer _attackCooldownTimer;

        public WarriorStateMachine(Enemy warriorEnemy)
        {
            WarriorEnemy = warriorEnemy;
            StatesCooldown = new Dictionary<Type, float>();
            
            IdleWarriorEnemyState = new IdleWarriorEnemyState(this);
            FollowWarriorEnemyState = new FollowWarriorEnemyState(this);
            PatrolWarriorEnemyState = new PatrolWarriorEnemyState(this);
            LightAttackWarriorEnemyState = new LightAttackWarriorEnemyState(this);
            DashFirstAttackWarriorEnemyState = new DashFirstAttackWarriorEnemyState(this);
            RotateTowardsEnemyState = new RotateTowardsTargetEnemyState(this);
            ForwardMoveWarriorEnemyState = new ForwardMoveWarriorEnemyState(this);
            StrafeMoveWarriorEnemyState = new StrafeMoveWarriorEnemyState(this);
            RollBackWarriorEnemyState = new RollBackWarriorEnemyState(this);
            RollBackWarriorEnemyState = new RollBackWarriorEnemyState(this);
            ComboFirstWarriorEnemyState = new ComboFirstWarriorEnemyState(this);
            DashSecondAttackWarriorEnemy = new DashSecondAttackWarriorEnemy(this);
            ComboSecondWarriorEnemyState = new ComboSecondWarriorEnemyState(this);

            _attackCooldownTimer = new AttackCooldownTimer();
            _attackCooldownTimer.Init(StatesCooldown);
        }

        public override IState StartState() => IdleWarriorEnemyState;

        public override void Update()
        {
            _attackCooldownTimer.Update(Time.deltaTime, CurrentStatesCooldown);
            StatesCooldown = _attackCooldownTimer.StatesCooldown;
            base.Update();
        }

        public void StartCooldown(Type state, float time)
        {
            if(!typeof(IState).IsAssignableFrom(state)) return;
            if(StatesCooldown.ContainsKey(state)) return;

            StatesCooldown.Add(state, time);
            CurrentStatesCooldown = new Dictionary<Type, float>(StatesCooldown);
        }
    }
}