using Data.States;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player", menuName = "Characters/PlayerDataState")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
        [field: SerializeField] public PlayerAirborneData AirborneData { get; private set; }
    }
}