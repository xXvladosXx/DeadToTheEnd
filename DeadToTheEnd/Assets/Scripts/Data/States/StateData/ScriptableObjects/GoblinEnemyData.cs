using Data.States.StateData.Enemy;
using Data.States.StateData.Goblin;
using StateMachine.Enemies.GoblinEnemy.States.Movement;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Goblin", menuName = "Characters/GoblinDataState")]
    public class GoblinEnemyData : EnemyData
    {
        [field: SerializeField] public GoblinFollowData GoblinFollowData { get; private set; }
        [field: SerializeField] public GoblinForwardMoveData GoblinForwardMoveData { get; private set; }
        [field: SerializeField] public GoblinLightAttackData GoblinLightAttackData { get; private set; }
        [field: SerializeField] public GoblinOrdinaryAttackData GoblinOrdinaryAttackData { get; private set; }
        [field: SerializeField] public GoblinHeavyAttackData GoblinHeavyAttackData { get; private set; }
        [field: SerializeField] public GoblinRangeAttackData GoblinRangeAttackData { get; private set; }
        [field: SerializeField] public GoblinFirstComboAttackData GoblinFirstComboAttackData { get; private set; }
        [field: SerializeField] public GoblinSecondComboAttackData GoblinSecondComboAttackData { get; private set; }
    }
}