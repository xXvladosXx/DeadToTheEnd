using Data.States.StateData;

namespace Data.States
{
    public class GhoulStateReusableData : IReusable
    {
        public bool IsBlocking { get; set; }
        public bool IsRolling { get; set; }
        public bool IsTargetBehind { get; set; }
        public bool IsRotatingWithRootMotion { get; set; }
        public bool IsPerformingAction { get; set; }
    }
}