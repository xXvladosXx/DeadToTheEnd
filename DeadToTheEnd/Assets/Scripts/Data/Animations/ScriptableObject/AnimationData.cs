using UnityEngine;

namespace Data.Animations.ScriptableObject
{
    [CreateAssetMenu (menuName = "AnimationData")]
    public abstract class AnimationData : UnityEngine.ScriptableObject
    {
        [SerializeField] private string _rotateBehindParameterName = "RotateBehind";
        [SerializeField] private string _rotateLeftParameterName = "RotateLeft";
        [SerializeField] private string _rotateRightParameterName = "RotateRight";
        [SerializeField] private string _verticalParameterName = "Vertical";
        [SerializeField] private string _horizontalParameterName = "Horizontal";
        [SerializeField] private string _movingParameterName = "Moving";

        [SerializeField] private string _idleParameterName = "isIdling";
        [SerializeField] private string _walkParameterName = "isWalking";
        [SerializeField] private string _runParameterName = "isRunning";
        [SerializeField] private string _sprintParameterName = "isSprinting";
        [SerializeField] private string _rollParameterName = "Roll";
        [SerializeField] private string _speedParameterName = "Speed";
        
        public int IdleParameterHash { get; private set; }
        public int WalkParameterHash { get; private set; }
        public int RunParameterHash { get; private set; }
        public int SprintParameterHash { get; private set; }
        public int RollParameterHash { get; private set; }
        public int MovingParameterHash { get; private set; }
        public int SpeedParameterHash { get; private set; }
        public int RotateBehindParameterHash { get; private set; }
        public int RotateLeftParameterHash { get; private set; }
        public int RotateRightParameterHash { get; private set; }
        public int VerticalParameterHash { get; private set; }
        public int HorizontalParameterHash { get; private set; }

        public virtual void Init()
        {
            MovingParameterHash = Animator.StringToHash(_movingParameterName);

            IdleParameterHash = Animator.StringToHash(_idleParameterName);
            WalkParameterHash = Animator.StringToHash(_walkParameterName);
            RunParameterHash = Animator.StringToHash(_runParameterName);
            SprintParameterHash = Animator.StringToHash(_sprintParameterName);
            RollParameterHash = Animator.StringToHash(_rollParameterName);
            SpeedParameterHash = Animator.StringToHash(_speedParameterName);
            
            VerticalParameterHash = Animator.StringToHash(_verticalParameterName);
            HorizontalParameterHash = Animator.StringToHash(_horizontalParameterName);
            RotateBehindParameterHash = Animator.StringToHash(_rotateBehindParameterName);
            RotateLeftParameterHash = Animator.StringToHash(_rotateLeftParameterName);
            RotateRightParameterHash = Animator.StringToHash(_rotateRightParameterName);
        }
    }
}