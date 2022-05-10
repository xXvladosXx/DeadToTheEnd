using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine
    {
        protected IState _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public virtual void Update()
        {
            Debug.Log(_currentState);
            _currentState?.Update();
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

    }
}