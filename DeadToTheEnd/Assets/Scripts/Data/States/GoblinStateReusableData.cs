using Data.States.StateData;

namespace Data.States
{
    public class GoblinStateReusableData : IReusable
    {
        public int MaxAmountOfBackHits { get; set; }
        public int MaxAmountOfFrontHits { get; set; }
        public int CurrentAmountOfBackHits { get; set; }
        public int CurrentAmountOfFrontHits { get; set; }
        public bool IsBlocking { get; set; }
        public bool IsRolling { get; set; }
        public bool IsTargetBehind { get; set; }
        public bool IsPerformingAction { get; set; }
        public bool IsRotatingWithRootMotion { get; set; }
        public bool CanStrafe { get; set; }
        public bool CanRoll { get; set; }
    }
}