﻿using Data.ScriptableObjects;
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

        protected override void Awake()
        {
            base.Awake();

            Reusable = new BlueDragonStateReusableData();
            BlueDragonStateReusableData = (BlueDragonStateReusableData) Reusable;

            Health = new Health(BlueDragonStateReusableData);
            StateMachine = new BlueDragonStateMachine(this);
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
        }
    }

    

    
}