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
        public DashAttackWarriorEnemyState DashAttackWarriorEnemyState { get; }
        public RotateTowardsTargetEnemyState RotateTowardsEnemyState { get; }
        public ForwardMoveWarriorEnemyState ForwardMoveWarriorEnemyState { get; }
        public StrafeMoveWarriorEnemyState StrafeMoveWarriorEnemyState { get; }
        public RollBackWarriorEnemyState RollBackWarriorEnemyState { get; }

        public Dictionary<IState, float> StatesCooldown { get; private set; }
        public Dictionary<IState, float> CurrentStatesCooldown { get; private set; }

        private Timer _timer;

        public WarriorStateMachine(Enemy warriorEnemy)
        {
            WarriorEnemy = warriorEnemy;
            StatesCooldown = new Dictionary<IState, float>();
            
            IdleWarriorEnemyState = new IdleWarriorEnemyState(this);
            FollowWarriorEnemyState = new FollowWarriorEnemyState(this);
            PatrolWarriorEnemyState = new PatrolWarriorEnemyState(this);
            LightAttackWarriorEnemyState = new LightAttackWarriorEnemyState(this);
            DashAttackWarriorEnemyState = new DashAttackWarriorEnemyState(this);
            RotateTowardsEnemyState = new RotateTowardsTargetEnemyState(this);
            ForwardMoveWarriorEnemyState = new ForwardMoveWarriorEnemyState(this);
            StrafeMoveWarriorEnemyState = new StrafeMoveWarriorEnemyState(this);
            RollBackWarriorEnemyState = new RollBackWarriorEnemyState(this);

            _timer = new Timer();
            _timer.Init(StatesCooldown);
        }

        public override void Update()
        {
            _timer.Update(Time.deltaTime, CurrentStatesCooldown);
            StatesCooldown = _timer.StatesCooldown;
            base.Update();
        }

        public void StartCooldown(IState state, float time)
        {
            if(StatesCooldown.ContainsKey(state)) return;

            StatesCooldown.Add(state, time);
            CurrentStatesCooldown = new Dictionary<IState, float>(StatesCooldown);
        }
    }
}