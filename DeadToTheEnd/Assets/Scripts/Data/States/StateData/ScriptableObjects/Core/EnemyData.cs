using Data.States.StateData.Enemy;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Goblin", menuName = "Characters/EnemyData")]
    public class EnemyData : EntityData
    {
        [field: SerializeField] public EnemyRollData EnemyRollData { get; private set; }
        [field: SerializeField] public EnemyWalkData EnemyWalkData { get; private set; }
        [field: SerializeField] public EnemyIdleData EnemyIdleData { get; private set; }
        [field: SerializeField] public EnemyHeavyAttackData EnemyHeavyAttackData { get; private set; }
        [field: SerializeField] public EnemyOrdinaryAttackData EnemyOrdinaryAttackData { get; private set; }
        [field: SerializeField] public EnemyRangeAttackData EnemyRangeAttackData { get; private set; }
    }
}