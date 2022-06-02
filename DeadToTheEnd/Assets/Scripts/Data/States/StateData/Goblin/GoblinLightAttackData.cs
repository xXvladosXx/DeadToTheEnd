using System;
using UnityEngine;

namespace Data.States.StateData.Goblin
{
    [Serializable]
    public class GoblinLightAttackData
    {
        [field: SerializeField] public float WalkSpeedModiferFirstAttack { get; private set; }
        [field: SerializeField] public float WalkSpeedModiferSecondAttack { get; private set; }
        [field: SerializeField] public float DistanceModifier { get; private set; }
    }
}