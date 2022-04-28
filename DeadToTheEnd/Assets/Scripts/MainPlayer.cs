using System;
using Data.Layers;
using Data.ScriptableObjects;
using Data.States;
using StateMachine.Player;
using UnityEngine;
using Utilities;
using Utilities.Collider;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public sealed class MainPlayer : MonoBehaviour
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }
    [field: SerializeField] public PlayerColliderUtility ColliderUtility { get; private set; }
    [field: SerializeField] public PlayerLayerData PlayerLayerData { get; private set; }
    
    private PlayerMovementStateMachine _playerMovementStateMachine;
    public PlayerStateReusableData ReusableData { get; set; }
    public Transform MainCamera { get; private set; }
    public PlayerInput InputAction { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        InputAction = GetComponent<PlayerInput>();
        Rigidbody = GetComponent<Rigidbody>();
        
        ColliderUtility.Init(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
        
        MainCamera = UnityEngine.Camera.main.transform;
        ReusableData = new PlayerStateReusableData();
        _playerMovementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void OnValidate()
    {
        ColliderUtility.Init(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
    }

    private void Start()
    {
        _playerMovementStateMachine.ChangeState(_playerMovementStateMachine.IdleState);
    }

    private void Update()
    {
        _playerMovementStateMachine.HandleInput();
        _playerMovementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        _playerMovementStateMachine.FixedUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerMovementStateMachine.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _playerMovementStateMachine.OnTriggerExit(other);
    }
}