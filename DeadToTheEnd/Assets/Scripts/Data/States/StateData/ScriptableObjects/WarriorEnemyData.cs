using Data.States.StateData.Enemy;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Warrior", menuName = "Characters/WarriorDataState")]
    public sealed class WarriorEnemyData : EnemyData
    {
        [field: SerializeField] public EnemyRunData EnemyRunData { get; private set; }
        [field: SerializeField] public EnemyPatrolData EnemyPatrolData { get; private set; }
        [field: SerializeField] public EnemyAttackData EnemyAttackData { get; private set; }
        [field: SerializeField] public EnemyDashData EnemyDashData { get; private set; }
        [field: SerializeField] public EnemyComboData EnemyComboData { get; private set; }
        [field: SerializeField] public EnemyStrafeData EnemyStrafeData { get; private set; }
    }
}