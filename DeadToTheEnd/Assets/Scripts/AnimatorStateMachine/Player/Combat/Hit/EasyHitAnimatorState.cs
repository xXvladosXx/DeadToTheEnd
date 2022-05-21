using UnityEngine;

namespace AnimatorStateMachine.Combat.Hit
{
    [CreateAssetMenu(menuName = "AnimatorState/EasyHitState")]
    public class EasyHitAnimatorState : HitAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            Player.Animator.SetBool(PlayerAnimationData.EasyHitParameterHash, false);
        }
    }
}