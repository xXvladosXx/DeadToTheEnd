using CameraManage;
using Data.Animations;
using Data.Combat;
using Data.ScriptableObjects;
using Entities.Core;
using Entities.Enemies;
using StateMachine.WarriorEnemy.States.Movement;
using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement
{
    public class BaseGoblinEnemyState : BaseEnemyState
    {
        protected GoblinEnemyAnimationData GoblinEnemyAnimationData;
        protected GoblinStateMachine GoblinStateMachine;
        protected GoblinEnemyData BossEnemyData;

        protected Entities.Enemies.GoblinEnemy GoblinEnemy;
        private float _curTime;
        private float _timeToWait;
        protected BaseGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
            GoblinStateMachine = stateMachine;

            GoblinEnemy = stateMachine.AliveEntity as Entities.Enemies.GoblinEnemy;
            BossEnemyData = GoblinEnemy.GoblinEnemyData;
            GoblinEnemyAnimationData = GoblinEnemy.GoblinEnemyAnimationData;
        }
        

        public override void Exit()
        {
            base.Exit();
            _curTime = 0;
        }

        public override void Update()
        {
            base.Update();
            _curTime += Time.deltaTime;
            float viewAngle = GetViewAngle(GoblinEnemy.transform,
                GoblinEnemy.MainPlayer.transform);

            /*if (viewAngle is > 45 or < -45)
            {
                GoblinEnemy.NavMeshAgent.isStopped = true;
                GoblinStateMachine.ChangeState(GoblinStateMachine.RotateGoblinEnemyState);
            }*/
        }

        protected override void AddEventCallbacks()
        {
            base.AddEventCallbacks();
            GoblinStateMachine.AliveEntity.Health.OnDamageTaken += HealthOnOnAttackApplied;
        }
        protected override void RemoveEventCallbacks()
        {
            base.RemoveEventCallbacks();
            GoblinStateMachine.AliveEntity.Health.OnDamageTaken -= HealthOnOnAttackApplied;
        }
        
        private void HealthOnOnAttackApplied(AttackData attackData)
        {
            CinemachineShake.Instance.ShakeCamera(.3f, .3f);
        }
    }
}