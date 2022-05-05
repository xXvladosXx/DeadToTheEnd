using System;
using Entities;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class EnemyStateReusableData
    {
        [field: SerializeField] public float MovementSpeedModifier { get; set; } = 1f;
        [field: SerializeField] public Vector3 StartPosition { get; set; }

        public void Initialize(Enemy enemy)
        {
            StartPosition = enemy.transform.position;
        }
    }
}