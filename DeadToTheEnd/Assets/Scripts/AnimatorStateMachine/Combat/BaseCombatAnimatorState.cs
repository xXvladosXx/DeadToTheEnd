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
            AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            Player = animator.GetComponent<MainPlayer>();
            PlayerAnimationData = Player.PlayerAnimationData;

            Player.ReusableData.CanMakeCombo = true;
            Player.ReusableData.ComboWasMade = false;
            Player.Animator.applyRootMotion = true;

            AddInputCallbacks();
        }


        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator,
            AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            TargetLocked();

            _time = stateInfo.normalizedTime;
            if (stateInfo.normalizedTime > _endTimeToMakeCombo)
            {
                Player.ReusableData.CanMakeCombo = false;
                RemoveInputCallbacks();
            }
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator,
            AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            RemoveInputCallbacks();
            Player.ReusableData.CanMakeCombo = true;

            if (!Player.ReusableData.ComboWasMade)
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

        private void OnComboCalled(InputAction.CallbackContext obj)
        {
            if (_time > _startTimeToMakeCombo && _time < _endTimeToMakeCombo)
            {
                Player.Animator.SetBool(PlayerAnimationData.ComboParameterHash, true);
                Player.ReusableData.ComboWasMade = true;
            }
        }

        private void TargetLocked()
        {
            if (Player.ReusableData.IsLocked)
            {
                Transform transform;
                (transform = Player.transform).LookAt(Player.ReusableData.Target);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }
    }
}