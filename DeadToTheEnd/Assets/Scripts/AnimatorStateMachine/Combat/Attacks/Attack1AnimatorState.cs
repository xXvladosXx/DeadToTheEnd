using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace AnimatorStateMachine.Combat.Attacks
{
    [CreateAssetMenu(menuName = "AnimatorState/Attack1State")]
    public class Attack1AnimatorState : BaseCombatAnimatorState
    {

        public override void OnExit(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnExit(characterState, animator, stateInfo, playerInputActions);
            Player.Animator.SetBool(Player.AnimationData.Attack1ParameterHash, false);
            Player.Animator.SetBool(Player.AnimationData.ComboParameterHash, false);
            Player.Animator.SetBool(Player.AnimationData.MovingParameterHash, false);
            Player.Animator.SetBool(Player.AnimationData.WasMovingParameterHash, false);
        }

    }
}