using Data.Animations;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Combat
{
    [CreateAssetMenu(menuName = "AnimatorState/BaseCombatState")]
    public class BaseCombatAnimatorState : BaseAnimatorState
    {
        [SerializeField] protected float _startTimeToMakeCombo;
        [SerializeField] protected float _endTimeToMakeCombo;

        protected MainPlayer Player;
        protected PlayerAnimationData PlayerAnimationData;
        private float _time;

        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator,
            AnimatorStateInfo stateInfo)
        {
            Player = animator.GetComponent<MainPlayer>();
            PlayerAnimationData = Player.PlayerAnimationData;

            Player.Animator.applyRootMotion = true;

            AddInputCallbacks();
        }


        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator,
            AnimatorStateInfo stateInfo)
        {
            TargetLocked();

            _time = stateInfo.normalizedTime;
            if (stateInfo.normalizedTime > _endTimeToMakeCombo)
            {
                RemoveInputCallbacks();
            }
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator,
            AnimatorStateInfo stateInfo)
        {
            RemoveInputCallbacks();

                Player.Animator.SetBool(PlayerAnimationData.ComboParameterHash, false);
        }

        protected virtual void AddInputCallbacks()
        {
            Player.InputAction.PlayerActions.Combo.performed += OnComboCalled;
        }

        protected virtual void RemoveInputCallbacks()
        {
            Player.InputAction.PlayerActions.Combo.performed -= OnComboCalled;
        }

        protected void ResetVelocity()
        {
            Player.Rigidbody.velocity = Vector3.zero;
        }

        private void OnComboCalled(InputAction.CallbackContext obj)
        {
            if (_time > _startTimeToMakeCombo && _time < _endTimeToMakeCombo)
            {
                Player.Animator.SetBool(PlayerAnimationData.ComboParameterHash, true);
            }
        }

        private void TargetLocked()
        {
                Transform transform;
                (transform = Player.transform).LookAt(Player.ReusableData.Target);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        
    }
}