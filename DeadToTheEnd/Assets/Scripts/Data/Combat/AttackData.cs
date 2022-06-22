using Data.Camera;
using Entities;
using Entities.Core;

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
        public AttackType AttackType { get; set; }
        public AliveEntity User { get; set; }
        public float Speed { get; set; }
        public ShakeCameraData ShakeCameraData { get; set; }
    }
}