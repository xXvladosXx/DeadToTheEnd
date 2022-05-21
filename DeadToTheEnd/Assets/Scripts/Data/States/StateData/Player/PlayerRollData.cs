using System;
using UnityEngine;

namespace Data.States.StateData.Player
{
    [Serializable]
    public class PlayerRollData
    {
        [field: SerializeField] public float SpeedModifier { get; set; } = 10;
    }
}