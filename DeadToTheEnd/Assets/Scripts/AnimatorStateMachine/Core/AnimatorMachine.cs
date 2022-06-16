using System.Collections.Generic;
using UnityEngine;

namespace AnimatorStateMachine.Core
{
    public class AnimatorMachine : StateMachineBehaviour
    {
        [SerializeField] private List<BaseAnimatorState> _animatorListData = new List<BaseAnimatorState>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var stateData in _animatorListData)
            {
                stateData.OnEnter(this, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var stateData in _animatorListData)
            {
                stateData.OnUpdate(this, animator, stateInfo);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var stateData in _animatorListData)
            {
                stateData.OnExit(this, animator, stateInfo);
            }
        }
    }
}