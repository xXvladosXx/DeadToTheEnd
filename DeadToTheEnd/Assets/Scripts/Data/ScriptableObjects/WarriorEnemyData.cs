using Data.States.StateData.Enemy;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Warrior", menuName = "Characters/WarriorDataState")]
    public sealed class WarriorEnemyData : ScriptableObject
    {
        [field: SerializeField] public EnemyRunData EnemyRunData { get; private set; }
        [field: SerializeField] public EnemyWalkData EnemyWalkData { get; private set; }
        [field: SerializeField] public EnemyIdleData EnemyIdleData { get; private set; }
        [field: SerializeField] public EnemyPatrolData EnemyPatrolData { get; private set; }
    }
}