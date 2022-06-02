using Combat.ColliderActivators;
using Data.Animations;
using Data.ScriptableObjects;
using Data.States;
using Data.Stats;
using StateMachine.Enemies.GoblinEnemy;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace Entities.Enemies
{
    public class GoblinEnemy : Enemy
    {
        [field: SerializeField] public GoblinEnemyData GoblinEnemyData { get; private set; }
        [field: SerializeField] public GoblinEnemyAnimationData GoblinEnemyAnimationData { get; private set; }
        
        public GoblinStateReusableData GoblinStateReusableData { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Reusable = new GoblinStateReusableData();
            GoblinStateReusableData = (GoblinStateReusableData) Reusable;
            
            Health = new Health(GoblinStateReusableData);
            GoblinEnemyAnimationData.Init();
            
            StateMachine = new GoblinStateMachine(this);
        }
        
        private void OnAnimatorMove()
        {
            //if (GoblinStateReusableData.IsRotatingWithRootMotion)
           // {
           //     transform.rotation *= Animator.deltaRotation;
          //      Rigidbody.velocity = Vector3.zero;
          //      return;
           // }
            
            float delta = Time.deltaTime;
            Rigidbody.drag = 0;
            Vector3 deltaPosition = Animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
        }
    }
}