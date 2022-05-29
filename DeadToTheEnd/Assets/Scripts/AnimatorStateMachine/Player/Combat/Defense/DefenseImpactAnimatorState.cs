using CameraManage;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AnimatorStateMachine.Movement.Grounded.Locked
{
    [CreateAssetMenu(menuName = "AnimatorState/DefenseImpactState")]
    public class DefenseImpactAnimatorState : DefenseAnimatorState
    {
        private Vector2 _lastDirection;
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseImpactParameterHash, false);
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(MainPlayer.InputAction.PlayerActions.MovementWithoutNormalization.ReadValue<Vector2>() != Vector2.zero)
                _lastDirection = MainPlayer.InputAction.PlayerActions.MovementWithoutNormalization.ReadValue<Vector2>();
            
            base.OnUpdate(characterState, animator, stateInfo);
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseImpactParameterHash, false);
        }
    }
}