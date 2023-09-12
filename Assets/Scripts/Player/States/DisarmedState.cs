using stateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DisarmedState : HierarchicalState
{
    private readonly PlayerView _playerView;
    private readonly PlayerInput _playerInput;
    private readonly Transform _camera;
    private readonly Action<CallbackContext> _onSprintStarted, _onSprintEnded, _onJumpPerformed;
    private Vector2 _moveVector;

    public DisarmedState(List<HierarchicalState> subStates, PlayerView playerVIew, PlayerInput inputs, Transform camera)
    {
        _playerInput = inputs;
        _playerView = playerVIew;
        _camera = camera;

        SubStates = subStates;
        _onSprintStarted = e => TrySetSubState<SprintingState>();
        _onSprintEnded = e => TrySetSubState<RunningState>();
        _onJumpPerformed = e => TrySetSubState<JumpingState>();

        _playerInput.Player.Move.performed += e => _moveVector = e.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += e => _moveVector = Vector2.zero;
    }

    protected override void OnEnter()
    {
        _playerView.SetDisarmed();
        _playerInput.Player.Sprint.started += _onSprintStarted;
        _playerInput.Player.Sprint.canceled += _onSprintEnded;
        _playerInput.Player.Jump.performed += _onJumpPerformed;
    }

    protected override void OnExit()
    {
        _playerInput.Player.Sprint.started -=_onSprintStarted;
        _playerInput.Player.Sprint.canceled -= _onSprintEnded;
        _playerInput.Player.Jump.performed -= _onJumpPerformed;
    }

    protected override void OnUpdate()
    {
        _playerView.Move(_moveVector);
        _playerView.Rotate(_camera.TransformDirection(new Vector3(_moveVector.x, 0, _moveVector.y)));
    }

    public override bool CanTransit(State state)
    {
        return state switch
        {
            ArmedState => true,
            _ => false,
        };
    }

    protected override void InitSubState() => TrySetSubState<RunningState>();
}
