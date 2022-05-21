using UnityEngine;

namespace AnimatorStateMachine.Combat.Hit
{
    [CreateAssetMenu(menuName = "AnimatorState/KnockdownHitState")]

    public class KnockdownAnimatorState : HitAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            Player.Animator.SetBool(PlayerAnimationData.KnockdownParameterHash, false);
            Player.ReusableData.IsKnockned = true;
        }

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            Player.ReusableData.IsKnockned = false;
        }
    }
}