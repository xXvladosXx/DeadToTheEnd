using Data.States.StateData.Enemy;
using Data.States.StateData.Goblin;
using StateMachine.Enemies.GoblinEnemy.States.Movement;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Goblin", menuName = "Characters/GoblinDataState")]
    public class GoblinEnemyData: ScriptableObject
    {
        [field: SerializeField] public GoblinFollowData GoblinFollowData { get; private set; }
        [field: SerializeField] public GoblinForwardMoveData GoblinForwardMoveData { get; private set; }
        [field: SerializeField] public EnemyRunData EnemyRunData { get; private set; }
        [field: SerializeField] public EnemyWalkData EnemyWalkData { get; private set; }
        [field: SerializeField] public EnemyIdleData EnemyIdleData { get; private set; }
        [field: SerializeField] public EnemyPatrolData EnemyPatrolData { get; private set; }
        [field: SerializeField] public EnemyAttackData EnemyAttackData { get; private set; }
        [field: SerializeField] public EnemyRollData EnemyRollData { get; private set; }
        [field: SerializeField] public EnemyDashData EnemyDashData { get; private set; }
        [field: SerializeField] public EnemyComboData EnemyComboData { get; private set; }
        [field: SerializeField] public EnemyStrafeData EnemyStrafeData { get; private set; }
    }
}