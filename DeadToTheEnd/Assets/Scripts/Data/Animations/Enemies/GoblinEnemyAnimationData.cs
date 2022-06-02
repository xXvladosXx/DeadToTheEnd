using System;
using UnityEngine;

namespace Data.Animations
{
    [Serializable]
    public class GoblinEnemyAnimationData : EnemyAnimationData
    {
        [SerializeField] private string _blockHitParameterName = "BlockHit";
        [SerializeField] private string _hitParameterName = "Hit";
        [SerializeField] private string _mediumHitParameterName = "MediumHit";
        [SerializeField] private string _attackParameterName = "Attack";
        [SerializeField] private string _lightAttackParameterName = "LightAttack";
        [SerializeField] private string _heavyAttackParameterName = "HeavyAttack";
        [SerializeField] private string _ordinaryAttackParameterName = "OrdinaryAttack";
        [SerializeField] private string _rangeAttackParameterName = "RangeAttack";

        public int BlockHitParameterHash { get; private set; }
        public int HitParameterHash { get; private set; }
        public int MediumHitParameterHash { get; private set; }
        public int AttackParameterHash { get; private set; }
        public int LightAttackParameterHash { get; private set; }
        public int OrdinaryAttackParameterHash { get; private set; }
        public int HeavyAttackParameterHash { get; private set; }
        public int RangeAttackParameterHash { get; private set; }

        public override void Init()
        {
            base.Init();

            MediumHitParameterHash = Animator.StringToHash(_mediumHitParameterName);
            HitParameterHash = Animator.StringToHash(_hitParameterName);
            BlockHitParameterHash = Animator.StringToHash(_blockHitParameterName);

            AttackParameterHash = Animator.StringToHash(_attackParameterName);
            LightAttackParameterHash = Animator.StringToHash(_lightAttackParameterName);
            OrdinaryAttackParameterHash = Animator.StringToHash(_ordinaryAttackParameterName);
            HeavyAttackParameterHash = Animator.StringToHash(_heavyAttackParameterName);
            RangeAttackParameterHash = Animator.StringToHash(_rangeAttackParameterName);
        }
    }
}