using UnityEngine;

namespace Data.Stats
{
    public class AttackSpeed : IStatsable
    {
        public float StartAttackSpeed { get; private set;} = 1;

        private Animator _animator;
        private StatsFinder _statsFinder;
        
        private float _lastAttackSpeed;
        private static readonly int Speed = Animator.StringToHash("AttackSpeed");

        public AttackSpeed(Animator animator, StatsFinder statsFinder)
        {
            _animator = animator;
            _statsFinder = statsFinder;
            
            _animator.SetFloat(Speed, _statsFinder.GetStat(Stat.AttackSpeed));
        }
        
        public void RecalculateStatWithMaxValue()
        {
            _animator.SetFloat(Speed, _statsFinder.GetStat(Stat.AttackSpeed));
        }

        public void RecalculateStatWithCurrentValue()
        {
            _lastAttackSpeed = _animator.GetFloat(Speed);
            _animator.SetFloat(Speed, _statsFinder.GetStat(Stat.AttackSpeed));
        }

        public float GetStatValue(Stat stat)
        {
            return 0;
        }

        public void Increase(float value)
        {
            
        }

        public void Decrease(float value)
        {
            
        }
        
        public Stat Stat => Stat.AttackSpeed;

    }
}