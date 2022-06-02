using System;
using Data.States.StateData;
using Entities;
using Entities.Enemies;
using UnityEngine;

namespace Data.States
{
    [Serializable]
    public sealed class EnemyStateReusableData : IReusable
    {
        [field: SerializeField] public Vector3 StartPosition { get; set; }
        [field: SerializeField] public bool IsPerformingAction { get; set; }
        
        public bool CanMakeDashAttack { get; set; }
        public bool CanStrafe { get; set; }
        public bool CanRoll { get; set; }
        public bool IsRotatingWithRootMotion { get; set; }

        public void Initialize(Enemy enemy)
        {
            StartPosition = enemy.transform.position;
        }

        public bool IsBlocking { get; set; }
        public bool IsRolling { get; set; }
        public bool IsTargetBehind { get; set; }
    }
}