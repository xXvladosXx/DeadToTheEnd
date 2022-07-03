using StateMachine.WarriorEnemy.Components;

namespace TimerSystem
{
    public interface ITimerController
    {
        CooldownTimer CooldownTimer { get; }
    }
}