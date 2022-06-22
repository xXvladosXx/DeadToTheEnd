using Data.Camera;
using Data.States;
using Data.States.StateData.Player;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player", menuName = "Characters/PlayerDataState")]
    public sealed class PlayerData : EntityData
    {
        [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    }
}