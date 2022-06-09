using Combat.ColliderActivators;
using Data.Combat;
using Data.ScriptableObjects;
using Data.States.StateData;
using Data.Stats;
using StateMachine.Enemies.BlueGragon;
using UnityEngine;

namespace Entities.Enemies
{
    public class BlueDragonEnemy : Enemy
    {
        [field: SerializeField] public BlueDragonEnemyData BlueDragonEnemyData { get; private set; }
        [field: SerializeField] public BlueDragonStateReusableData BlueDragonStateReusableData { get; private set; }

        public RadiusAttackColliderActivator RadiusAttackColliderActivator { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();

            Reusable = new BlueDragonStateReusableData();
            BlueDragonStateReusableData = (BlueDragonStateReusableData) Reusable;
            
            AttackCalculator = new AttackCalculator(BlueDragonStateReusableData);
            StateMachine = new BlueDragonStateMachine(this);

            RadiusAttackColliderActivator = GetComponentInChildren<RadiusAttackColliderActivator>();
        }
        
        private void OnAnimatorMove()
        {
            if (BlueDragonStateReusableData.IsRotatingWithRootMotion)
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
            Rigidbody.AddForce(velocity);
        }

        public void OnRadiusAttack(float time, AttackType knock)
        {
            RadiusAttackColliderActivator.enabled = true;
            AttackData attackData = new AttackData
            {
                AttackType = knock,
                User = this,
                Damage = Damage
            };
            
            RadiusAttackColliderActivator.ActivateCollider(time, attackData);
        }
    }

    

    
}