using System;
using Data.ScriptableObjects;
using StateMachine.Player;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public sealed class MainPlayer : MonoBehaviour
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }

    
    private PlayerMovementStateMachine _playerMovementStateMachine;
    public Transform MainCamera { get; private set; }
    public PlayerInput InputAction { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        InputAction = GetComponent<PlayerInput>();
        Rigidbody = GetComponent<Rigidbody>();

        MainCamera = UnityEngine.Camera.main.transform;
        _playerMovementStateMachine = new PlayerMovementStateMachine(this);
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
}