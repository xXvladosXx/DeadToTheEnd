using System;
using System.Collections.Generic;
using System.Linq;
using Combat.ColliderActivators;
using Data.Combat;
using Data.Stats;
using Entities;
using StatsSystem;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(MainPlayer))]
    public class HitCounter : MonoBehaviour
    {
        [SerializeField] private float _timeSinceLastHit = 1.6f;
        
        [field: SerializeField] public int Counter { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }

        private List<AttackColliderActivator> _attackColliderActivators;

        private float _lastHit;
        private AttackCalculator _attackCalculator;
        private Rage _rage;

        public event Action OnCounterChanged;
        public event Action OnCounterReset;
        private void Awake()
        {
            _attackColliderActivators = GetComponentsInChildren<AttackColliderActivator>().ToList();
            _attackCalculator = GetComponent<MainPlayer>().AttackCalculator;
            _rage = GetComponent<MainPlayer>().Rage;

            _attackCalculator.OnDamageTaken += ResetCounter;
            foreach (var attackColliderActivator in _attackColliderActivators)
            {
                attackColliderActivator.OnTargetHit += IncreaseHitValue;
            }
        }

        private void Update()
        {
            if(_lastHit <= 0) return;
            
            _lastHit -= Time.deltaTime;
            if (_lastHit <= 0)
            {
                ResetCounter(null);
            }
        }

        private void IncreaseHitValue(AttackData attackData)
        {
            _lastHit = _timeSinceLastHit;
            _rage.AddRagePoints();
            Damage += attackData.Damage;
            Counter++;
            
            OnCounterChanged?.Invoke();
        }
        
        private void ResetCounter(AttackData attackData)
        {
            _lastHit = 0;
            _rage.RemoveRagePoints();

            Damage = 0;
            Counter = 0;
            
            OnCounterReset?.Invoke();
        }
    }
}