using Data.Camera;
using Entities;
using Entities.Core;
using UnityEngine;

namespace Data.Combat
{
    public enum AttackType
    {
        Knock,
        Medium,
        Easy
    }
    
    public class AttackData
    {
        public float Damage { get; set; }
        public float CriticalDamage { get; set; }
        public float CriticalChance { get; set; }
        public AttackType AttackType { get; set; }
        public AliveEntity User { get; set; }
        public float Speed { get; set; }
        public ShakeCameraData ShakeCameraData { get; set; }
    }
}