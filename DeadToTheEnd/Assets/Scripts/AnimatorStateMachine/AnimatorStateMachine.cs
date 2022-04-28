using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public class AnimatorStateMachine : StateMachineBehaviour
    {
        [SerializeField] private List<BaseAnimatorState> _animatorListData = new List<BaseAnimatorState>();
        private PlayerInput _playerInputActions;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerInputActions = animator.GetComponent<PlayerInput>();
            foreach (var stateData in _animatorListData)
            {
                stateData.OnEnter(this, animator, stateInfo, _playerInputActions);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var stateData in _animatorListData)
            {
                stateData.OnUpdate(this, animator, stateInfo, _playerInputActions);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var stateData in _animatorListData)
            {
                stateData.OnExit(this, animator, stateInfo, _playerInputActions);
            }
        }
    }
}