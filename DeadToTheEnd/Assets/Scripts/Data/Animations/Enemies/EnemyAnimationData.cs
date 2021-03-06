using System;
using Data.Animations.ScriptableObject;
using StateMachine.WarriorEnemy;
using UnityEngine;

namespace Data.Animations
{
    [CreateAssetMenu (menuName = "AnimationData/EnemyAnimationData")]
    public class EnemyAnimationData : AnimationData
    {
        [SerializeField] private string _continueAttackParameterName = "ContinueCombo";
        [SerializeField] private string _ordinaryAttackParameterName = "OrdinaryAttack";
        [SerializeField] private string _attackParameterName = "Attack";
        [SerializeField] private string _heavyAttackParameterName = "HeavyAttack";
        [SerializeField] private string _rangeAttackParameterName = "RangeAttack";
        [SerializeField] private string _deathParameterName = "Death";

        public int OrdinaryAttackParameterHash { get; private set; }
        public int AttackParameterHash { get; private set; }
        public int HeavyAttackParameterHash { get; private set; }
        public int RangeAttackParameterHash { get; private set; }

        public int ContinueComboAttackParameterHash { get; private set; }
        public int DeathParameterHash { get; private set; }

        public override void Init()
        {
            base.Init();
            ContinueComboAttackParameterHash = Animator.StringToHash(_continueAttackParameterName);
            OrdinaryAttackParameterHash = Animator.StringToHash(_ordinaryAttackParameterName);
            AttackParameterHash = Animator.StringToHash(_attackParameterName);
            HeavyAttackParameterHash = Animator.StringToHash(_heavyAttackParameterName);
            RangeAttackParameterHash = Animator.StringToHash(_rangeAttackParameterName);
            DeathParameterHash = Animator.StringToHash(_deathParameterName);
        }
    }
}