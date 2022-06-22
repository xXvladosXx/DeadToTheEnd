using Data.ScriptableObjects;
using Data.States;
using Data.Stats;
using StateMachine.Enemies.BlueGragon;
using UnityEngine;

namespace Entities.Enemies
{
    public class GhoulEnemy : Enemy
    {
        [field: SerializeField] public EnemyData EnemyData { get; private set; }
        [field: SerializeField] public GhoulStateReusableData GhoulStateReusableData { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Reusable = new GhoulStateReusableData();
            GhoulStateReusableData = (GhoulStateReusableData) Reusable;

            AttackCalculator = new AttackCalculator(GhoulStateReusableData);
            StateMachine = new StandardEnemyStateMachine(this);
        }
    }
}