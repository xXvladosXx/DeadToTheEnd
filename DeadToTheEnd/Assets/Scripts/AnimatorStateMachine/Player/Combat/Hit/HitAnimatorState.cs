using AnimatorStateMachine.Movement;
using UnityEngine;

namespace AnimatorStateMachine.Combat.Hit
{
    public abstract class HitAnimatorState : BaseMovementAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            Player.Animator.applyRootMotion = true;
            Player.ReusableData.WasHit = true;
            Player.ReusableData.IsTargetLocked = true;
        }
        
        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            Player.Animator.applyRootMotion = false;
            Player.Animator.SetBool(PlayerAnimationData.DefenseParameterHash, false);
            Player.ReusableData.WasHit = false;
            Player.ReusableData.IsTargetLocked = false;

            ResetVelocity();
        }
    }
}