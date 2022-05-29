using AnimatorStateMachine.Movement;
using UnityEngine;

namespace AnimatorStateMachine.Combat.Hit
{
    public abstract class HitAnimatorState : BaseMovementAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            MainPlayer.Animator.applyRootMotion = true;
            MainPlayer.ReusableData.WasHit = true;
        }
        
        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            MainPlayer.Animator.applyRootMotion = false;
            MainPlayer.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, false);
            MainPlayer.ReusableData.WasHit = false;

            ResetVelocity();
        }
    }
}