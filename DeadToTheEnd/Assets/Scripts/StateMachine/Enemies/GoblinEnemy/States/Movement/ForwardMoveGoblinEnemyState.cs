using UnityEngine;

namespace StateMachine.Enemies.GoblinEnemy.States.Movement
{
    public class ForwardMoveGoblinEnemyState : BaseGoblinEnemyState
    {
        private float _curTime;
        private float _timeToMoveForward;

        public ForwardMoveGoblinEnemyState(GoblinStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            GoblinEnemy.NavMeshAgent.isStopped = false;
            GoblinEnemy.NavMeshAgent.speed = GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.WalkSpeedModifer;
            _timeToMoveForward = DecideTimeOfMoving(GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.WalkMinTime,
                GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.WalkMaxTime);
        }

        public override void Exit()
        {
            base.Exit();
            GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.VerticalParameterHash, 0);
        }

        public override void Update()
        {
            //if (GoblinEnemy.GoblinStateReusableData.IsPerformingAction) return;
            _curTime += Time.deltaTime;
            base.Update();
            
            if (IsEnoughDistance(5, GoblinStateMachine.AliveEntity.transform, 
                    GoblinEnemy.Target.transform))
            {
                GoblinStateMachine.ChangeState(GoblinStateMachine.LightAttackGoblinEnemyState);
            }
                //DecideAttackToDo();

                HandleMoveToTarget();
        }
       
        private void HandleMoveToTarget()
        {
            if (IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.DistanceToStop,
                    GoblinEnemy.transform,
                    GoblinEnemy.Target.transform))
            {
                //DecideAttackToDo();
                Stop();
                GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.VerticalParameterHash, 0, .1f, Time.deltaTime);
                if(GoblinEnemy.Animator.GetFloat(GoblinEnemyAnimationData.VerticalParameterHash) <= .01)
                    GoblinStateMachine.ChangeState(GoblinStateMachine.FollowGoblinEnemyState);
                
                return;
            }

            if (!IsEnoughDistance(GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.DistanceToStop,
                    GoblinEnemy.transform,
                    GoblinEnemy.Target.transform))
            {
                GoblinEnemy.Animator.SetFloat(GoblinEnemyAnimationData.VerticalParameterHash, 1, .1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget();
            TargetLocked();
        }

        private void HandleRotateTowardsTarget()
        {
            if (GoblinEnemy.GoblinStateReusableData.IsPerformingAction)
            {
                Vector3 direction = GoblinEnemy.Target.transform.position -
                                    GoblinEnemy.transform.position;

                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = GoblinStateMachine.AliveEntity.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                GoblinStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(
                    GoblinStateMachine.AliveEntity.transform.rotation, targetRotation,
                    GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.RotationSpeedModifer / Time.deltaTime);
            }
            else
            {
                Vector3 targetVelocity = GoblinEnemy.Rigidbody.velocity;

                GoblinEnemy.NavMeshAgent.enabled = true;
                GoblinEnemy.NavMeshAgent.isStopped = false;
                GoblinEnemy.NavMeshAgent.SetDestination(GoblinEnemy
                    .Target.transform.position);
                GoblinEnemy.Rigidbody.velocity = targetVelocity;

                GoblinStateMachine.AliveEntity.transform.rotation = Quaternion.Slerp(
                    GoblinEnemy.transform.rotation,
                    GoblinEnemy.NavMeshAgent.transform.rotation,
                    GoblinEnemy.GoblinEnemyData.GoblinForwardMoveData.RotationSpeedModifer / Time.deltaTime);
            }
        }
    }
}