using CameraManage;
using Data.Stats;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Movement.Grounded.Locked
{
    [CreateAssetMenu(menuName = "AnimatorState/DefenseState")]
    public class DefenseAnimatorState : LockedAnimatorState
    {
        [SerializeField] private float _shakeIntensity = 2f;

        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            
            Player.Health.OnAttackApplied += OnDefenseImpact;
            
            Player.ReusableData.IsBlocking = true;
            Player.ReusableData.MovementSpeedModifier = 0f;
            
            Player.DefenseColliderActivator.ActivateCollider();
            Player.AttackColliderActivator.enabled = false;
        }
        
        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            TargetLocked();
            ReadMovementInputWithNormalization();
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            Player.Animator.SetBool(PlayerAnimationData.DefenseImpactParameterHash, false);
            
            Player.Health.OnAttackApplied -= OnDefenseImpact;

            Player.ReusableData.IsMovingAfterStop = true;
            Player.ReusableData.IsBlocking = false;
            
            Player.DefenseColliderActivator.DeactivateCollider();
        }

        protected override void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Dash.canceled += OnDefenseCanceled;
            Player.InputAction.PlayerActions.Attack1.performed += OnAttackCalled;
        }
        
        protected override void RemoveInputCallbacks()
        {
            Player.InputAction.PlayerActions.Dash.canceled -= OnDefenseCanceled;
            Player.InputAction.PlayerActions.Attack1.performed -= OnAttackCalled;
        }
      
        private void OnAttackCalled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }
        private void OnDefenseImpact()
        {
            CinemachineShake.Instance.ShakeCamera(_shakeIntensity, .5f);

            Player.Animator.SetBool(PlayerAnimationData.DefenseImpactParameterHash, true);
        }
        private void OnDefenseCanceled(InputAction.CallbackContext obj)
        {
            Player.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, false);
        }
        
    }
}