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

        public void HandleInput()
        {
            _currentState?.HandleInput();
        }

        public void Update()
        {
            Debug.Log(_currentState);
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}