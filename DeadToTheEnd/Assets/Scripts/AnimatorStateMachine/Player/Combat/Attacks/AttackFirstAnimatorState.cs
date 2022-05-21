using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Combat.Attacks
{
    [CreateAssetMenu(menuName = "AnimatorState/Attack1State")]
    public class AttackFirstAnimatorState : BaseCombatAnimatorState
    {

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnExit(characterState, animator, stateInfo);
            Player.Animator.SetBool(Player.PlayerAnimationData.Attack1ParameterHash, false);
            Player.Animator.SetBool(Player.PlayerAnimationData.ComboParameterHash, false);
            Player.Animator.SetBool(Player.PlayerAnimationData.MovingParameterHash, false);
            Player.Animator.SetBool(Player.PlayerAnimationData.WasMovingParameterHash, false);
        }

    }
}