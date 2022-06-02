using Data.States.StateData;

namespace Data.States
{
    public class GoblinStateReusableData : IReusable
    {
        public bool IsBlocking { get; set; }
        public bool IsRolling { get; set; }
        public bool IsTargetBehind { get; set; }
        public bool IsPerformingAction { get; set; }
        public bool CanStrafe { get; set; }
        public bool CanRoll { get; set; }
    }
}