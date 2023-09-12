using stateMachine;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatesFactory
{
    private readonly PlayerView _playerView;
    private readonly PlayerInput _inputs;
    private readonly Transform _target;
    private readonly GunHandler _gunHandler;
    private readonly Transform _camera;
    public PlayerStatesFactory(PlayerView playerView, PlayerInput inputs, Transform target, GunHandler gunHandler, Transform camera) 
    {
        _playerView = playerView;
        _inputs = inputs;
        _target = target;
        _gunHandler = gunHandler;
        _camera = camera;
    }

    public List<HierarchicalState> GetStates()
    {
        var runningState = new RunningState(_playerView);
        var sprintingState = new SprintingState(_playerView);
        var aimingState = new AimingState(_playerView);
        var jumpingState = new JumpingState(_playerView);
        return new List<HierarchicalState>() {
            new DisarmedState(
                new List<HierarchicalState>() {
                    runningState,
                    sprintingState,
                    jumpingState }, _playerView, _inputs, _camera),
            new ArmedState(
                new List<HierarchicalState>() {
                    runningState,
                    sprintingState,
                    aimingState,
                    jumpingState
                }, _gunHandler, _playerView, _inputs, _target ) };
    }
}
