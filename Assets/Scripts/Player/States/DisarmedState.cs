using stateMachine;
using System;
using UnityEngine;
using static GameInput;

public class DisarmedState : State
{
    private readonly PlayerView _playerView;
    private readonly PlayerActions _playerInput;
    private readonly Transform _camera;
    private Vector2 _moveVector;

    public DisarmedState(PlayerView playerVIew, PlayerActions inputs, Transform camera)
    {
        _playerInput = inputs;
        _playerView = playerVIew;
        _camera = camera;

        _playerInput.Move.performed += e => _moveVector = e.ReadValue<Vector2>();
        _playerInput.Move.canceled += e => _moveVector = Vector2.zero;
    }

    protected override void OnExit() { }

    protected override void OnUpdate()
    {
        _playerView.Move(_moveVector);
        _playerView.Rotate(_camera.TransformDirection(new Vector3(_moveVector.x, 0, _moveVector.y)));
    }

    protected override void OnEnter() => _playerView.SetDisarmed();
}
