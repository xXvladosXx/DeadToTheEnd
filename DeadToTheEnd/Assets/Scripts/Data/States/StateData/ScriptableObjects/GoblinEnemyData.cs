﻿using Data.States.StateData.Enemy;
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
        [field: SerializeField] public EnemyRollData EnemyRollData { get; private set; }
        [field: SerializeField] public GoblinLightAttackData GoblinLightAttackData { get; private set; }

    }
}