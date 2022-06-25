using Entities.Core;

namespace StateMachine.Core
{
    public abstract class EntityStateMachine : StateMachine
    {
        public AliveEntity AliveEntity { get; protected set; }
    }
}