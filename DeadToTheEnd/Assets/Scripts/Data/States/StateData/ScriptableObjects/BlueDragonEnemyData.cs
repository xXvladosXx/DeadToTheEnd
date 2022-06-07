using Data.States.StateData.BlueDragon;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Goblin", menuName = "Characters/BlueDragonDataState")]

    public class BlueDragonEnemyData : EnemyData
    {
        [field: SerializeField] public BlueDragonOrdinaryAttackData BlueDragonOrdinaryAttackData { get; private set; }
    }
}