using System.Collections.Generic;
using AnimatorStateMachine;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class AnimatorStateMachine : StateMachineBehaviour
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