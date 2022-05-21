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
    }
}