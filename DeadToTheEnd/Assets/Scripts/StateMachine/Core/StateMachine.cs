using System;
using System.Collections.Generic;
using Entities;
using Entities.Core;
using StateMachine.WarriorEnemy.Components;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine
    {
        
        public Dictionary<Type, float> StatesCooldown { get; private set; }
        protected Dictionary<Type, float> CurrentStatesCooldown { get; private set; }
        public StateCooldownTimer StateCooldownTimer { get; private set; }
        
        private IState _currentState;

        protected StateMachine()
        {
            StatesCooldown = new Dictionary<Type, float>();

            StateCooldownTimer = new StateCooldownTimer();
            StateCooldownTimer.Init(StatesCooldown);
        }

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
       Debug.Log(_currentState);
            _currentState?.Update();
            StateCooldownTimer.Update(Time.deltaTime, CurrentStatesCooldown);
            StatesCooldown = StateCooldownTimer.StatesCooldown;
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
        
        public void StartCooldown(Type state, float time)
        {
            if(!typeof(IState).IsAssignableFrom(state)) return;
            if(StatesCooldown.ContainsKey(state)) return;

            StatesCooldown.Add(state, time);
            CurrentStatesCooldown = new Dictionary<Type, float>(StatesCooldown);
        }
    }
}