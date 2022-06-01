using System;
using UnityEngine;

namespace Data.Animations
{
    [Serializable]
    public class WarriorEnemyAnimationData : EnemyAnimationData
    {
        [SerializeField] private string _comboFirstParameterName = "ComboAttack1";
        [SerializeField] private string _comboSecondParameterName = "ComboAttack2";

        [SerializeField] private string _lightAttackParameterName = "LightAttack";
        [SerializeField] private string _dashAttackParameterName = "DashAttack";
        [SerializeField] private string _dashSecondAttackParameterName = "DashAttack2";
        [SerializeField] private string _sprintAttackParameterName = "SprintAttack";
        
        public int LightAttackParameterHash { get; private set; }
        public int DashAttackParameterHash { get; private set; }
        public int DashSecondAttackParameterHash { get; private set; }
        public int SprintAttackParameterHash { get; private set; }
        public int ComboFirstParameterHash { get; private set; }
        public int ComboSecondParameterHash { get; private set; }
        
        public override void Init()
        {
            base.Init();
            LightAttackParameterHash = Animator.StringToHash(_lightAttackParameterName);
            ComboFirstParameterHash = Animator.StringToHash(_comboFirstParameterName);
            ComboSecondParameterHash = Animator.StringToHash(_comboSecondParameterName);
            SprintAttackParameterHash = Animator.StringToHash(_sprintAttackParameterName);
            DashAttackParameterHash = Animator.StringToHash(_dashAttackParameterName);
            DashSecondAttackParameterHash = Animator.StringToHash(_dashSecondAttackParameterName);
        }
    }
}