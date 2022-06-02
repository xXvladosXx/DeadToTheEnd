using Data.Camera;
using Data.States;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player", menuName = "Characters/PlayerDataState")]
    public sealed class PlayerData : ScriptableObject
    {
        [field: SerializeField] public PlayerShakeCameraData PlayerShakeCameraData { get; private set; }
        [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    }
}