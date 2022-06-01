using Data.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine.Player.States.Movement.Grounded.Combat
{
    public class PlayerAttackState : PlayerGroundedState
    {
        private bool _stopRotating;
        public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            StartAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = true;
            _stopRotating = false;
        }
        
        public override void Exit()
        {
            base.Exit();
            
            StopAnimation(PlayerAnimationData.Attack1ParameterHash);
            MainPlayer.Animator.applyRootMotion = false;
        }

        public override void Update()
        {
            base.Update();
            
            if(_stopRotating) return;
            RotateToPoint();
        }

        public override void FixedUpdate()
        {
            Float();
        }

        protected override void OnLockedPerformed(InputAction.CallbackContext obj)
        {
        }


        public override void OnAnimationEnterEvent()
        {
            base.OnAnimationEnterEvent();
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();

            _stopRotating = true;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
        }
        
        public override void OnAnimationHandleEvent()
        {
            base.OnAnimationHandleEvent();
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
        }
        protected override void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.ReusableData.ShouldCombo = true;

            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerComboAttackState);
        }

        private void RotateToPoint()
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, MainPlayer.PlayerLayerData.AimLayer))
            {
                mouseWorldPosition = raycastHit.point;
            }
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = MainPlayer.transform.position.y;
            Vector3 aimDirection = (worldAimTarget - MainPlayer.transform.position).normalized;

            MainPlayer.transform.forward = Vector3.Lerp(MainPlayer.transform.forward, aimDirection, Time.deltaTime * 20f);
        }
    }
}