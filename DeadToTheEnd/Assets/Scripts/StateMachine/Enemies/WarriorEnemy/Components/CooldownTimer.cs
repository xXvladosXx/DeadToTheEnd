using System;
using System.Collections.Generic;
using Data.States;
using StateMachine.Core;
using UnityEngine;

namespace StateMachine.WarriorEnemy.Components
{
    [Serializable]
    public class CooldownTimer
    {
        public Dictionary<Type, float> StatesCooldown { get; private set; }
        public void Init(Dictionary<Type, float> statesCooldown)
        {
            StatesCooldown = statesCooldown;
        }

        public void StartCooldown(Type type, float time)
        {
            if(!typeof(ITimeable).IsAssignableFrom(type)) return;
            if(StatesCooldown.ContainsKey(type)) return;

            StatesCooldown.Add(type, time);
        }
        
        public void Update(float time, Dictionary<Type, float> currentStatesCooldown)
        {
            if (StatesCooldown.Count == 0) return;

            foreach (var state in StatesCooldown)
            {
                currentStatesCooldown[state.Key] -= time;
                if (state.Value < 0)
                {
                    currentStatesCooldown.Remove(state.Key);
                }
            }

            StatesCooldown = new Dictionary<Type, float>(currentStatesCooldown);
        }
    }
}