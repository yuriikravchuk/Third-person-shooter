using stateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ArmedState : HierarchicalState // rotate towards camera
{
    private readonly PlayerView _playerView;
    private readonly PlayerInput _playerInput;
    private readonly GunHandler _gunHandler;
    private readonly Transform _targetTransform;
    private Vector2 _moveVector;
    private bool ShootButtonPressed = false;
    private readonly Action<CallbackContext> _onSprintStarted, _onSprintEnded, _onJumpPerformed;

    public ArmedState(List<HierarchicalState> subStates, GunHandler gunHandler,PlayerView playerView, PlayerInput playerInput, Transform targetTransform)
    {
        SubStates = subStates;
        _playerView = playerView;
        _playerInput = playerInput;
        _targetTransform = targetTransform;
        _gunHandler = gunHandler;
        _onSprintStarted = e => TrySetSubState<SprintingState>();
        _onSprintEnded = e => TrySetSubState<RunningState>();
        _onJumpPerformed = e => TrySetSubState<JumpingState>();
        _playerInput.Player.Aim.Disable();
        _playerInput.Player.Shoot.started += e => ShootButtonPressed = true;
        _playerInput.Player.Shoot.canceled += e => ShootButtonPressed = false;
        _playerInput.Player.Aim.started += e => TrySetSubState<AimingState>();
        _playerInput.Player.Aim.canceled += e => TrySetSubState<RunningState>();
        _playerInput.Player.First_Weapon.performed += e => _gunHandler.OnGunSelected(0);
        _playerInput.Player.Second_Weapon.performed += e => _gunHandler.OnGunSelected(1);
        _playerInput.Player.Third_Weapon.performed += e => _gunHandler.OnGunSelected(2);
    }

    protected override void OnEnter()
    {
        _playerView.SetArmed();
        _playerInput.Player.Sprint.started += _onSprintStarted;
        _playerInput.Player.Sprint.canceled += _onSprintEnded;
        _playerInput.Player.Jump.performed += _onJumpPerformed;
        _playerInput.Player.Aim.Enable();
        _playerInput.Player.Move.performed += e => _moveVector = e.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += e => _moveVector = Vector2.zero;
    }

    protected override void OnExit()
    {
        _playerView.SetDisarmed();
        _playerInput.Player.Sprint.started -= _onSprintStarted;
        _playerInput.Player.Sprint.canceled -= _onSprintEnded;
        _playerInput.Player.Jump.performed -= _onJumpPerformed;
        _playerInput.Player.Aim.Disable();
    }

    protected override void OnUpdate()
    {
        _playerView.Rotate(_targetTransform.position - _playerView.transform.position);
        _playerView.Move(_moveVector);

        if (ShootButtonPressed && SubState is SprintingState == false)
            _gunHandler.TryFire();
    }


    public override bool CanTransit(State state)
    {
        if (SubState != null && SubState.CanTransit(state)) return true;

        return state switch
        {
            DisarmedState => true,
            _ => false,
        };
    }

    protected override void OnSubStateChanged(HierarchicalState state)
    {
        switch(state)
        {
            case RunningState:
                _playerView.Aim();
                break;
            case SprintingState:
                _playerView.StopAiming();
                break;
            case AimingState:
                _playerView.Aim();
                break;
            case JumpingState:
                _playerView.StopAiming();
                break;
        }
    }
}