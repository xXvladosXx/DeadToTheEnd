using DefaultNamespace;
using UnityEngine;
using Utilities;

namespace AnimatorStateMachine.Movement.Grounded.Moving
{
    [CreateAssetMenu(menuName = "AnimatorState/MoveState")]
    public class RunningAnimatorState : BaseMovementAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            base.OnEnter(characterState, animator, stateInfo);
            
            Player.ReusableData.MovementSpeedModifier = GroundedData.PlayerRunData.SpeedModifier;
        }
    }
}