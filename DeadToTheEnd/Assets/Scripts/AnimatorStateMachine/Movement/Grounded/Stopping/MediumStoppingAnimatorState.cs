using UnityEngine;
using Utilities;

namespace AnimatorStateMachine.Movement.Grounded.Stopping
{
    [CreateAssetMenu(menuName = "AnimatorState/MediumStopState")]
    public class MediumStoppingAnimatorState : StoppingAnimatorState
    {
        public override void OnEnter(DefaultNamespace.AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions)
        {
            base.OnEnter(characterState, animator, stateInfo, playerInputActions);
            Player.ReusableData.MovementDecelerationForce = GroundedData.StopData.MediumDeceleration;
        }
    }
}