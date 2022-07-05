using System;
using System.Collections.Generic;
using System.Linq;
using CameraManage;
using Data.Animations;
using Data.Camera;
using Data.Combat;
using Data.ScriptableObjects;
using Data.States;
using Data.States.StateData.Player;
using Entities;
using InventorySystem;
using LootSystem;
using SkillsSystem;
using StateMachine.Player.States.Movement.Grounded.Combat;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Layer;
using Utilities.Raycast;

namespace StateMachine.Player.States.Movement
{
    public abstract class PlayerMovementState : IState
    {
        protected PlayerStateMachine PlayerStateMachine;

        protected PlayerAnimationData PlayerAnimationData;

        protected readonly MainPlayer MainPlayer;
        protected readonly PlayerGroundData PlayerGroundData;

        private float _calculateTime;
        private SkillManager _skillManager;

        public PlayerMovementState(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;

            MainPlayer = PlayerStateMachine.MainPlayer;
            var data = PlayerStateMachine.MainPlayer.EntityData as PlayerData;
            PlayerGroundData = data.GroundData;
            PlayerAnimationData = MainPlayer.PlayerAnimationData;
            _skillManager = MainPlayer.GetComponent<SkillManager>();

            InitializeData();
        }

        private void InitializeData()
        {
            SetBaseCameraRecenteringData();

            SetBaseRotationData();
        }

        public virtual void Enter()
        {
            AddInputCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputCallbacks();
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
            Move();
        }

        public virtual void TriggerOnStateAnimationEnterEvent()
        {
        }

        public virtual void TriggerOnStateAnimationExitEvent()
        {
        }

        public virtual void TriggerOnStateAnimationHandleEvent()
        {
        }

        public virtual void HandleInput()
        {
            if (!MainPlayer.PlayerStateReusable.StopReading)
                ReadMovementInput();
        }

        private void ReadMovementInput()
        {
            MainPlayer.PlayerStateReusable.MovementInputWithNormalization =
                MainPlayer.InputAction.PlayerActions.Movement.ReadValue<Vector2>();
            MainPlayer.PlayerStateReusable.MovementInputWithoutNormalization = MainPlayer.InputAction.PlayerActions
                .MovementWithoutNormalization.ReadValue<Vector2>();
        }

        protected virtual void AddInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Movement.performed += OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Movement.canceled += OnMovementCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed += OnAttackPerformed;
            MainPlayer.InputAction.PlayerActions.Locked.performed += OnLockedPerformed;

            MainPlayer.InputAction.PlayerActions.FirstSkillCast.performed += OnFirstSkillPerformed;
            MainPlayer.InputAction.PlayerActions.SecondSkillCast.performed += OnSecondSkillPerformed;
            MainPlayer.InputAction.PlayerActions.ThirdSkillCast.performed += OnThirdSkillPerformed;
            MainPlayer.InputAction.PlayerActions.FourthSkillCast.performed += OnFourthSkillPerformed;
            MainPlayer.InputAction.PlayerActions.FifthSkillCast.performed += OnFifthSkillPerformed;

            MainPlayer.AttackCalculator.OnDamageTaken += OnDamageTaken;

            MainPlayer.InputAction.PlayerActions.FirstItem.performed += OnFirstItemUsed;
            MainPlayer.InputAction.PlayerActions.SecondItem.performed += OnSecondItemUsed;
            MainPlayer.InputAction.PlayerActions.ThirdItem.performed += OnThirdItemUsed;
            MainPlayer.InputAction.PlayerActions.FourthItem.performed += OnFourthItemUsed;
        }

        

        private void OnFirstItemUsed(InputAction.CallbackContext obj)
        {
            MainPlayer.ItemEquipper.TryToUseItem(MainPlayer, 0);
        }

        private void OnSecondItemUsed(InputAction.CallbackContext obj)
        {
            MainPlayer.ItemEquipper.TryToUseItem(MainPlayer, 1);
        }

        private void OnThirdItemUsed(InputAction.CallbackContext obj)
        {
            MainPlayer.ItemEquipper.TryToUseItem(MainPlayer, 2);
        }

        private void OnFourthItemUsed(InputAction.CallbackContext obj)
        {
            MainPlayer.ItemEquipper.TryToUseItem(MainPlayer, 3);
        }

        protected virtual void OnFirstSkillPerformed(InputAction.CallbackContext obj)
        {
            TryToApplySkill(0);
        }

        protected virtual void OnSecondSkillPerformed(InputAction.CallbackContext obj)
        {
            TryToApplySkill(1);
        }

        protected virtual void OnThirdSkillPerformed(InputAction.CallbackContext obj)
        {
            TryToApplySkill(2);
        }

        protected virtual void OnFourthSkillPerformed(InputAction.CallbackContext obj)
        {
            TryToApplySkill(3);
        }

        protected virtual void OnFifthSkillPerformed(InputAction.CallbackContext obj)
        {
            TryToApplySkill(4);
        }

        protected virtual void OnLockedPerformed(InputAction.CallbackContext obj)
        {
            if (!TryToChangeWeapon(false)) return;

            if (MainPlayer.TryGetComponent(out EnemyLockOn enemyLockOn))
            {
                if (!enemyLockOn.FindTarget()) return;

                MainPlayer.PlayerStateReusable.LockedState = true;
                MainPlayer.PlayerStateReusable.Target = enemyLockOn.ScanNearBy();
                MainPlayer.PlayerStateReusable.Target.Health.OnDied += ResetTarget;

                MainPlayer.LongSwordActivator.ActivateSword();

                PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerLockedMovementState);
                MainPlayer.Animator.SetLayerWeight(1, 1);

                foreach (var shortSwordActivator in MainPlayer.ShortSwordsActivator)
                    shortSwordActivator.DeactivateSword();

                CinemachineCameraSwitcher.Instance.ChangeCamera();
            }
        }


        protected void ResetTarget()
        {
            TryToChangeWeapon(true);
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerIdleState);
            StopAnimation(PlayerAnimationData.LockedParameterHash);

            MainPlayer.LongSwordActivator.DeactivateSword();
            MainPlayer.PlayerStateReusable.LockedState = false;
            MainPlayer.GetComponent<EnemyLockOn>().ResetTarget();
            MainPlayer.PlayerStateReusable.Target = null;
            MainPlayer.Animator.SetLayerWeight(1, 0);

            foreach (var shortSwordActivator in MainPlayer.ShortSwordsActivator)
                shortSwordActivator.ActivateSword();

            CinemachineCameraSwitcher.Instance.ChangeCamera();
        }

        protected virtual void OnDamageTaken(AttackData attackData)
        {
            if (MainPlayer.PlayerStateReusable.IsKnocked) return;

            MainPlayer.PlayerStateReusable.LastHitFromTarget = attackData.User.transform;
            switch (attackData.AttackType)
            {
                case AttackType.Knock:
                    PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerKnockHitState);
                    break;
                case AttackType.Medium:
                    // PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerMediumHitState);
                    break;
                case AttackType.Easy:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            MainPlayer.PlayerStateReusable.ShouldAttack = true;
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            DisableCameraRecentering();
        }

        protected virtual void OnMovementPerformed(InputAction.CallbackContext obj)
        {
        }

        protected virtual void RemoveInputCallbacks()
        {
            MainPlayer.InputAction.PlayerActions.Movement.performed -= OnMovementPerformed;
            MainPlayer.InputAction.PlayerActions.Movement.canceled -= OnMovementCanceled;
            MainPlayer.InputAction.PlayerActions.Attack.performed -= OnAttackPerformed;
            MainPlayer.InputAction.PlayerActions.Locked.performed -= OnLockedPerformed;

            MainPlayer.InputAction.PlayerActions.FirstSkillCast.performed -= OnFirstSkillPerformed;
            MainPlayer.InputAction.PlayerActions.SecondSkillCast.performed -= OnSecondSkillPerformed;
            MainPlayer.InputAction.PlayerActions.ThirdSkillCast.performed -= OnThirdSkillPerformed;
            MainPlayer.InputAction.PlayerActions.FourthSkillCast.performed -= OnFourthSkillPerformed;

            MainPlayer.AttackCalculator.OnDamageTaken -= OnDamageTaken;

            MainPlayer.InputAction.PlayerActions.FirstItem.performed -= OnFirstItemUsed;
            MainPlayer.InputAction.PlayerActions.SecondItem.performed -= OnSecondItemUsed;
            MainPlayer.InputAction.PlayerActions.ThirdItem.performed -= OnThirdItemUsed;
            MainPlayer.InputAction.PlayerActions.FourthItem.performed -= OnFourthItemUsed;
        }

        protected void SetBaseCameraRecenteringData()
        {
            MainPlayer.PlayerStateReusable.SideCameraRecenteringDatas = PlayerGroundData.SideCameraRecenteringDatas;
            MainPlayer.PlayerStateReusable.BackCameraRecenteringDatas = PlayerGroundData.BackCameraRecenteringDatas;
        }

        protected void SetBaseRotationData()
        {
            MainPlayer.PlayerStateReusable.RotationData = PlayerGroundData.BaseRotationData;
            MainPlayer.PlayerStateReusable.TimeToReachTargetRotation =
                MainPlayer.PlayerStateReusable.RotationData.TargetRotationReachTimeDefault;
        }

        protected void StartAnimation(int animation)
        {
            MainPlayer.Animator.SetBool(animation, true);
        }

        protected void StopAnimation(int animation)
        {
            MainPlayer.Animator.SetBool(animation, false);
        }

        private void Move()
        {
            if (MainPlayer.PlayerStateReusable.MovementInputWithNormalization == Vector2.zero ||
                MainPlayer.PlayerStateReusable.MovementSpeedModifier == 0f)
            {
                MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, 0f,
                    0.05f, UnityEngine.Time.deltaTime);
                MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, false);
                return;
            }

            var movementDirection = GetMovementInputDirection();

            var targetRotationAngle = Rotate(movementDirection);

            var targetRotationDirection = GetTargetRotationDirection(targetRotationAngle);
            var movementSpeed = GetSmoothMovementSpeed();

            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            MainPlayer.Animator.SetBool(PlayerAnimationData.MovingParameterHash, true);
            MainPlayer.Animator.SetBool(PlayerAnimationData.WasMovingParameterHash, true);
            MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash,
                MainPlayer.PlayerStateReusable.MovementSpeedModifier,
                0.5f, UnityEngine.Time.deltaTime);

            MainPlayer.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity,
                ForceMode.VelocityChange);
        }

        protected void LookAt(Transform target)
        {
            Transform transform;
            (transform = MainPlayer.transform).LookAt(target);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        private float GetSmoothMovementSpeed()
        {
            _calculateTime += UnityEngine.Time.deltaTime;

            float movementSpeed = GetMaxMovementSpeed();

            if (MainPlayer.PlayerStateReusable.IsMovingAfterStop)
            {
                movementSpeed = Mathf.Lerp(0, GetMaxMovementSpeed(), _calculateTime);
            }

            if (GetMaxMovementSpeed() == movementSpeed)
            {
                MainPlayer.PlayerStateReusable.IsMovingAfterStop = false;
            }

            return movementSpeed;
        }

        protected void ResetAnimatorSpeed()
        {
            _calculateTime = 0;
            MainPlayer.Animator.SetFloat(PlayerAnimationData.SpeedParameterHash, 0);
        }

        protected float GetMaxMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = PlayerGroundData.BaseSpeed * MainPlayer.PlayerStateReusable.MovementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= MainPlayer.PlayerStateReusable.MovementSlopeSpeedModifier;
            }

            return movementSpeed;
        }

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(MainPlayer.PlayerStateReusable.MovementInputWithNormalization.x, 0f,
                MainPlayer.PlayerStateReusable.MovementInputWithNormalization.y);
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != MainPlayer.PlayerStateReusable.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        private float AddCameraRotationToAngle(float angle)
        {
            angle += MainPlayer.MainCamera.eulerAngles.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            MainPlayer.PlayerStateReusable.CurrentTargetRotation.y = targetAngle;

            MainPlayer.PlayerStateReusable.DampedTargetRotationPassedTime.y = 0f;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = MainPlayer.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == MainPlayer.PlayerStateReusable.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle,
                MainPlayer.PlayerStateReusable.CurrentTargetRotation.y,
                ref MainPlayer.PlayerStateReusable.DampedTargetRotationCurrentVelocity.y,
                MainPlayer.PlayerStateReusable.TimeToReachTargetRotation.y
                - MainPlayer.PlayerStateReusable.DampedTargetRotationPassedTime.y);

            MainPlayer.PlayerStateReusable.DampedTargetRotationPassedTime.y += UnityEngine.Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            MainPlayer.Rigidbody.MoveRotation(targetRotation);
        }

        protected void RotateToPoint()
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

            MainPlayer.transform.forward =
                Vector3.Lerp(MainPlayer.transform.forward, aimDirection, Time.deltaTime * 20f);
        }

        protected Vector3 GetTargetRotationDirection(float targetRotationAngle)
        {
            return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
        }

        protected float GetMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = PlayerGroundData.BaseSpeed * MainPlayer.PlayerStateReusable.MovementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= MainPlayer.PlayerStateReusable.MovementSlopeSpeedModifier;
            }

            return movementSpeed;
        }


        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = MainPlayer.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3(0f, MainPlayer.Rigidbody.velocity.y, 0f);
        }

        protected void DisableCameraRecentering()
        {
            MainPlayer.CameraUtility.DisableRecentering();
        }

        protected void ResetVelocity()
        {
            MainPlayer.Rigidbody.velocity = Vector3.zero;
        }

        protected void ResetVerticalVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            MainPlayer.Rigidbody.velocity = playerHorizontalVelocity;
        }

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            MainPlayer.Rigidbody.AddForce(
                -playerHorizontalVelocity * MainPlayer.PlayerStateReusable.MovementDecelerationForce,
                ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            Vector3 playerHorizontaVelocity = GetPlayerHorizontalVelocity();

            Vector2 playerHorizontalMovement = new Vector2(playerHorizontaVelocity.x, playerHorizontaVelocity.z);

            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }

        private void TryToApplySkill(int index)
        {
            if (!_skillManager.TryToApplySkill(index, MainPlayer)) return;

            MainPlayer.PlayerStateReusable.SkillAnimToPlay = _skillManager.LastAppliedSkill.Anim.AnimName;
            PlayerStateMachine.ChangeState(PlayerStateMachine.PlayerThirdSkillCastState);
        }

        private bool TryToChangeWeapon(bool toShortSword)
        {
            return MainPlayer.ItemEquipper.TryToChangeWeapon(toShortSword);
            Debug.Log("Changed");
        }
    }
}