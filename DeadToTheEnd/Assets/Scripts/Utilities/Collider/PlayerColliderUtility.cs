using System;
using Data.Collider;
using UnityEngine;

namespace Utilities.Collider
{
    [Serializable]
    public class PlayerColliderUtility : CapsuleColliderUtility
    {
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }
    }
}