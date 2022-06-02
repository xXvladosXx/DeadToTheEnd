using Entities;
using Entities.Core;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine
    {
        protected IState _currentState;
        public AliveEntity AliveEntity { get; protected set; }

        public abstract IState StartState();
        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public virtual void Update()
        {
//                Debug.Log(_currentState);
            _currentState?.Update();
        }

        public void HandleInput()
        {
            _currentState?.HandleInput();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
        
        public void OnAnimationEnterEvent()
        {
            _currentState?.OnAnimationEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            _currentState?.OnAnimationExitEvent();
        }

        public void OnAnimationHandleEvent()
        {
            _currentState?.OnAnimationHandleEvent();
        }
    }
}