using System;
using Data.Animations;
using Data.ScriptableObjects;
using Data.States;
using Data.Stats;
using StateMachine.Enemies.WarriorEnemy;
using UnityEngine;

namespace Entities.Enemies
{
    public class WarriorEnemy : Enemy
    {
        [field: SerializeField] public WarriorEnemyData WarriorEnemyData { get; private set; }
        [field: SerializeField] public WarriorStateReusableData WarriorStateReusableData { get; private set; }


        protected override void Awake()
        {
            base.Awake();

            WarriorStateReusableData.Initialize(this);
            Reusable = WarriorStateReusableData;

            AttackCalculator = new AttackCalculator(WarriorStateReusableData);
            
            StateMachine = new WarriorStateMachine(this);
        }
        
        private void OnAnimatorMove()
        {
            if (WarriorStateReusableData.IsRotatingWithRootMotion)
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