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
            
            MainPlayer.Health.OnAttackApplied += OnDefenseImpact;
            
            MainPlayer.ReusableData.IsBlocking = true;
            MainPlayer.ReusableData.MovementSpeedModifier = 0f;
            
            //MainPlayer.DefenseColliderActivator.ActivateCollider();
            //MainPlayer.AttackColliderActivator.enabled = false;
        }
        
        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            TargetLocked();
            ReadMovementInputWithNormalization();
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseImpactParameterHash, false);
            
            MainPlayer.Health.OnAttackApplied -= OnDefenseImpact;

            MainPlayer.ReusableData.IsMovingAfterStop = true;
            MainPlayer.ReusableData.IsBlocking = false;
            
            MainPlayer.DefenseColliderActivator.DeactivateCollider();
        }

        protected override void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.canceled += OnDefenseCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackCalled;
        }
        
        protected override void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Dash.canceled -= OnDefenseCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackCalled;
        }
      
        private void OnAttackCalled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.Attack1ParameterHash, true);
        }
        private void OnDefenseImpact()
        {
            CinemachineShake.Instance.ShakeCamera(_shakeIntensity, .5f);

            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseImpactParameterHash, true);
        }
        private void OnDefenseCanceled(InputAction.CallbackContext obj)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, false);
        }
        
    }
}