using UnityEngine;

namespace AnimatorStateMachine.Core
{
    public abstract class BaseAnimatorState : ScriptableObject
    {
        public abstract void OnEnter(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo);

        public abstract void OnUpdate(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo);

        public abstract void OnExit(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo);

        
    }
}