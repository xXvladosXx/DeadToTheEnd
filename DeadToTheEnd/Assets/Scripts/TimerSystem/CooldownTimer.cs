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
        public Dictionary<Type, float> Cooldowns { get; private set; }
        public void Init(Dictionary<Type, float> statesCooldown)
        {
            Cooldowns = statesCooldown;
        }

        public void StartCooldown(Type type, float time)
        {
            if(!typeof(ITimeable).IsAssignableFrom(type)) return;
            if(Cooldowns.ContainsKey(type)) return;

            Cooldowns.Add(type, time);
        }
        
        public void Update(float time, Dictionary<Type, float> currentCooldowns)
        {
            if (Cooldowns.Count == 0) return;

            foreach (var state in Cooldowns)
            {
                currentCooldowns[state.Key] -= time;
                if (state.Value < 0)
                {
                    currentCooldowns.Remove(state.Key);
                }
            }

            Cooldowns = new Dictionary<Type, float>(currentCooldowns);
        }

        public float GetCooldownValue(Type type)
        {
            if (Cooldowns.TryGetValue(type, out float pct))
            {
                return pct;
            }

            return 0;
        }
    }
}