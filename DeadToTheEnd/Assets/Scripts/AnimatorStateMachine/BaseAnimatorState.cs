using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Utilities.PlayerInput;

namespace DefaultNamespace
{
    public abstract class BaseAnimatorState : ScriptableObject
    {
        public abstract void OnEnter(AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions);
        public abstract void OnUpdate(AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions);
        public abstract void OnExit(AnimatorStateMachine characterState, Animator animator, AnimatorStateInfo stateInfo,
            PlayerInput playerInputActions);

        protected RaycastHit GetRaycast(UnityEngine.Camera camera, LayerMask layerMask)
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMask))
            {
                return raycastHit;
            }

            return default;
        }
    }
}