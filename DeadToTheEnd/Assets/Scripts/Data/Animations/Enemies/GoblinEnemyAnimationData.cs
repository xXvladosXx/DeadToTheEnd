using System;
using UnityEngine;

namespace Data.Animations
{
    [CreateAssetMenu (menuName = "AnimationData/GoblinAnimationData")]

    public class GoblinEnemyAnimationData : EnemyAnimationData
    {
        [SerializeField] private string _blockHitParameterName = "BlockHit";
        [SerializeField] private string _hitParameterName = "Hit";
        [SerializeField] private string _mediumHitParameterName = "MediumHit";
        [SerializeField] private string _lightAttackParameterName = "LightAttack";
        [SerializeField] private string _rangeAttackParameterName = "RangeAttack";
        [SerializeField] private string _firstAttackParameterName = "FirstCombo";
        [SerializeField] private string _secondAttackParameterName = "SecondCombo";

        public int BlockHitParameterHash { get; private set; }
        public int HitParameterHash { get; private set; }
        public int MediumHitParameterHash { get; private set; }
        public int LightAttackParameterHash { get; private set; }
        public int RangeAttackParameterHash { get; private set; }
        public int FirstComboAttackParameterHash { get; private set; }
        public int SecondComboAttackParameterHash { get; private set; }

        public override void Init()
        {
            base.Init();

            MediumHitParameterHash = Animator.StringToHash(_mediumHitParameterName);
            HitParameterHash = Animator.StringToHash(_hitParameterName);
            BlockHitParameterHash = Animator.StringToHash(_blockHitParameterName);

            LightAttackParameterHash = Animator.StringToHash(_lightAttackParameterName);
            RangeAttackParameterHash = Animator.StringToHash(_rangeAttackParameterName);
            FirstComboAttackParameterHash = Animator.StringToHash(_firstAttackParameterName);
            SecondComboAttackParameterHash = Animator.StringToHash(_secondAttackParameterName);
        }
    }
}