namespace Data.States.StateData
{
    public interface IReusable
    {
        bool IsBlocking { get; set; }
        bool IsRolling { get; set; }
        bool IsTargetBehind { get; set; }
        bool IsRotatingWithRootMotion { get; set; }
        bool IsPerformingAction { get; set; }
    }
}