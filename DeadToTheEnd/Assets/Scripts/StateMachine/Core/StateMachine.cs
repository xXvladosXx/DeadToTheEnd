using System;
using System.Collections.Generic;
using Entities;
using Entities.Core;
using StateMachine.Core;
using StateMachine.WarriorEnemy.Components;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine
    {
        public Dictionary<Type, float> StatesCooldown { get; private set; }
        private Dictionary<Type, float> CurrentStatesCooldown { get; set; }
        private CooldownTimer CooldownTimer { get; }
        
        protected IState _currentState;

        protected StateMachine()
        {
            StatesCooldown = new Dictionary<Type, float>();

            CooldownTimer = new CooldownTimer();
            CooldownTimer.Init(StatesCooldown);
        }


        public abstract IState StartState();
        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public  void Update()
        {
            _currentState?.Update();
            CooldownTimer.Update(Time.deltaTime, CurrentStatesCooldown);
            StatesCooldown = CooldownTimer.Cooldowns;
        }

        public void HandleInput()
        {
            _currentState?.HandleInput();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
        
        public void TriggerOnStateAnimationEnterEvent()
        {
            _currentState?.TriggerOnStateAnimationEnterEvent();
        }

        public void TriggerOnStateAnimationExitEvent()
        {
            _currentState?.TriggerOnStateAnimationExitEvent();
        }

        public void TriggerOnStateAnimationHandleEvent()
        {
            _currentState?.TriggerOnStateAnimationHandleEvent();
        }
        
        public void StartCooldown(Type state, float time)
        {
            CooldownTimer.StartCooldown(state, time);
            CurrentStatesCooldown = new Dictionary<Type, float>(StatesCooldown);
        }
    }
}