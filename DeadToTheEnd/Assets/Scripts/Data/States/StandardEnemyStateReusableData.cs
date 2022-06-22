namespace Data.States.StateData
{
    public class StandardEnemyStateReusableData : IReusable
    {
        public bool IsBlocking { get; set; }
        public bool IsRolling { get; set; }
        public bool IsTargetBehind { get; set; }
        public bool IsPerformingAction { get; set; }
        public bool IsRotatingWithRootMotion { get; set; }
    }
}