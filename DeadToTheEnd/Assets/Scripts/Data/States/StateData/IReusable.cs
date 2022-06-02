namespace Data.States.StateData
{
    public interface IReusable
    {
        bool IsBlocking { get; set; }
        bool IsRolling { get; set; }
        bool IsTargetBehind { get; set; }
    }
}