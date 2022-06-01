using Data.Animations;
using Data.ScriptableObjects;
using Data.States;
using Data.Stats;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace Entities.Enemies
{
    public class BossEnemy : Enemy
    {
        [field: SerializeField] public BossEnemyData BossEnemyData { get; private set; }
        [field: SerializeField] public EnemyStateReusableData EnemyStateReusableData { get; private set; }
        [field: SerializeField] public WarriorEnemyAnimationData WarriorEnemyAnimationData { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            EnemyStateReusableData.Initialize(this);

            Health = new Health(EnemyStateReusableData);
            WarriorEnemyAnimationData.Init();
            
            StateMachine = new WarriorStateMachine(this);
        }
        
        private void OnAnimatorMove()
        {
            if (EnemyStateReusableData.IsRotatingWithRootMotion)
            {
                transform.rotation *= Animator.deltaRotation;
                Rigidbody.velocity = Vector3.zero;
                return;
            }
            
            float delta = Time.deltaTime;
            Rigidbody.drag = 0;
            Vector3 deltaPosition = Animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
        }
    }
}