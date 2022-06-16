using System;
using UnityEngine;

namespace Data.Animations
{
    [Serializable]
    public class PlayerAnimationData
    {
        [SerializeField] private string _groundedParameterName = "Grounded";
        [SerializeField] private string _movingParameterName = "Moving";
        [SerializeField] private string _stoppingParameterName = "Stopping";
        [SerializeField] private string _landingParameterName = "Landing";
        [SerializeField] private string _airborneParameterName = "Airborne";

        [SerializeField] private string _idleParameterName = "isIdling";
        [SerializeField] private string _dashParameterName = "isDashing";
        [SerializeField] private string _walkParameterName = "isWalking";
        [SerializeField] private string _runParameterName = "isRunning";
        [SerializeField] private string _sprintParameterName = "isSprinting";
        [SerializeField] private string _mediumStopParameterName = "isMediumStopping";
        [SerializeField] private string _hardStopParameterName = "isHardStopping";
        [SerializeField] private string _rollParameterName = "Roll";
        [SerializeField] private string _hardLandParameterName = "isHardLanding";
        [SerializeField] private string _wasMovingParameterName = "WasMoving";
        [SerializeField] private string _speedParameterName = "Speed";
        [SerializeField] private string _comboParameterName = "Combo";

        [SerializeField] private string _attackFirstParameterName = "Attack1";
        [SerializeField] private string _sprintAttackParameterName = "SprintAttack";

        [SerializeField] private string _fallParameterName = "isFalling";
        [SerializeField] private string _lockedParameterName = "IsLocked";
        [SerializeField] private string _defenseParameterName = "Defense";
        [SerializeField] private string _defenseImpactParameterName = "ImpactBlock";
        [SerializeField] private string _verticalParameterName = "Vertical";
        [SerializeField] private string _horizontalParameterName = "Horizontal";
        
        [SerializeField] private string _easyHitParameterName = "EasyHit";
        [SerializeField] private string _mediumHitParameterName = "MediumHit";
        [SerializeField] private string _knockdownParameterName = "Knockdown";
        
        [SerializeField] private string _skillCastParameterName = "SkillCast";
        [SerializeField] private string _secondSkillCastParameterName = "SecondSkill";
        [SerializeField] private string _thirdSkillCastParameterName = "ThirdSkill";
        [SerializeField] private string _fourthSkillCastParameterName = "FourthSkill";
        public int GroundedParameterHash { get; private set; }
        public int MovingParameterHash { get; private set; }
        public int WasMovingParameterHash { get; private set; }
        public int StoppingParameterHash { get; private set; }
        public int LandingParameterHash { get; private set; }
        public int AirborneParameterHash { get; private set; }
        public int EasyHitParameterHash { get; private set; }
        public int MediumHitParameterHash { get; private set; }
        public int KnockdownParameterHash { get; private set; }
        public int SpeedParameterHash { get; private set; }
        public int Attack1ParameterHash { get; private set; }
        public int SprintAttackParameterHash { get; private set; }
        public int ComboParameterHash { get; private set; }

        public int IdleParameterHash { get; private set; }
        public int DashParameterHash { get; private set; }
        public int WalkParameterHash { get; private set; }
        public int RunParameterHash { get; private set; }
        public int SprintParameterHash { get; private set; }
        public int MediumStopParameterHash { get; private set; }
        public int HardStopParameterHash { get; private set; }
        public int RollParameterHash { get; private set; }
        public int HardLandParameterHash { get; private set; }

        public int FallParameterHash { get; private set; }
        public int LockedParameterHash { get; private set; }
        public int DefenseParameterHash { get; private set; }
        public int DefenseImpactParameterHash { get; private set; }
        public int VerticalParameterHash { get; private set; }
        public int HorizontalParameterHash { get; private set; }
        public int SkillParameterHash { get; private set; }
        public int SecondSkillParameterHash { get; private set; }
        public int ThirdSkillParameterHash { get; private set; }
        public int FourthSkillParameterHash { get; private set; }

        public void Init()
        {
            GroundedParameterHash = Animator.StringToHash(_groundedParameterName);
            MovingParameterHash = Animator.StringToHash(_movingParameterName);
            StoppingParameterHash = Animator.StringToHash(_stoppingParameterName);
            LandingParameterHash = Animator.StringToHash(_landingParameterName);
            AirborneParameterHash = Animator.StringToHash(_airborneParameterName);

            IdleParameterHash = Animator.StringToHash(_idleParameterName);
            DashParameterHash = Animator.StringToHash(_dashParameterName);
            WalkParameterHash = Animator.StringToHash(_walkParameterName);
            RunParameterHash = Animator.StringToHash(_runParameterName);
            SprintParameterHash = Animator.StringToHash(_sprintParameterName);
            MediumStopParameterHash = Animator.StringToHash(_mediumStopParameterName);
            HardStopParameterHash = Animator.StringToHash(_hardStopParameterName);
            RollParameterHash = Animator.StringToHash(_rollParameterName);
            HardLandParameterHash = Animator.StringToHash(_hardLandParameterName);
            WasMovingParameterHash = Animator.StringToHash(_wasMovingParameterName);
            SpeedParameterHash = Animator.StringToHash(_speedParameterName);
            
            Attack1ParameterHash = Animator.StringToHash(_attackFirstParameterName);
            ComboParameterHash = Animator.StringToHash(_comboParameterName);
            SprintAttackParameterHash = Animator.StringToHash(_sprintAttackParameterName);

            FallParameterHash = Animator.StringToHash(_fallParameterName);
            LockedParameterHash = Animator.StringToHash(_lockedParameterName);
            DefenseParameterHash = Animator.StringToHash(_defenseParameterName);
            HorizontalParameterHash = Animator.StringToHash(_horizontalParameterName);
            VerticalParameterHash = Animator.StringToHash(_verticalParameterName);
            DefenseImpactParameterHash = Animator.StringToHash(_defenseImpactParameterName);

            EasyHitParameterHash = Animator.StringToHash(_easyHitParameterName);
            MediumHitParameterHash = Animator.StringToHash(_mediumHitParameterName);
            KnockdownParameterHash = Animator.StringToHash(_knockdownParameterName);

            SkillParameterHash = Animator.StringToHash(_skillCastParameterName);
            SecondSkillParameterHash = Animator.StringToHash(_secondSkillCastParameterName);
            FourthSkillParameterHash = Animator.StringToHash(_fourthSkillCastParameterName);
            ThirdSkillParameterHash = Animator.StringToHash(_thirdSkillCastParameterName);
        }
    }
}