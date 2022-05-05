using DefaultNamespace;
using UnityEngine;
using Utilities;

namespace AnimatorStateMachine.Movement.Grounded.Moving
{
    [CreateAssetMenu(menuName = "AnimatorState/MoveState")]
    public class RunningAnimatorState : BaseMovementAnimatorState
    {

        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnEnter(characterState, animator, stateInfo, playerInputActions);
            
            Player.ReusableData.MovementSpeedModifier = GroundedData.RunData.SpeedModifier;
        }

        public override void OnUpdate(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            Player.ReusableData.MovementSpeedModifier = GroundedData.RunData.SpeedModifier;
            base.OnUpdate(characterState, animator, stateInfo, playerInputActions);
        }
        
        
    }
}