using System;
using Entities;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class EnemyStateReusableData
    {
        [field: SerializeField] public float MovementSpeedModifier { get; set; } = 1f;
        [field: SerializeField] public float DashAttackCooldown { get; set; } = 4f;
        [field: SerializeField] public float ComboAttackCooldown { get; set; } = 4f;
        [field: SerializeField] public Vector3 StartPosition { get; set; }
        [field: SerializeField] public bool IsPerformingAction { get; set; }
        
        public bool CanMakeDashAttack { get; set; }
        public bool CanStrafe { get; set; }
        public bool CanRoll { get; set; }
        public bool IsRotatingWithRootMotion { get; set; }
        public Vector3 LocalVelocity { get; set; }

        public void Initialize(Enemy enemy)
        {
            StartPosition = enemy.transform.position;
        }
    }
}