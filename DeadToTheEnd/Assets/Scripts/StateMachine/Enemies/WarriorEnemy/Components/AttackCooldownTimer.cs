using System;
using System.Collections.Generic;
using Data.States;
using UnityEngine;

namespace StateMachine.WarriorEnemy.Components
{
    [Serializable]
    public class AttackCooldownTimer
    {
        public Dictionary<Type, float> StatesCooldown { get; private set; }
        public void Init(Dictionary<Type, float> statesCooldown)
        {
            StatesCooldown = statesCooldown;
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