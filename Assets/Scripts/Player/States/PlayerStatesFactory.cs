using stateMachine;
using System.Collections.Generic;
using UnityEngine;
using static GameInput;

public class PlayerStatesFactory
{
    private readonly PlayerView _playerView;
    private readonly PlayerActions _inputs;
    private readonly Transform _target;
    private readonly GunHandler _gunHandler;
    private readonly Transform _camera;
    public PlayerStatesFactory(PlayerView playerView, PlayerActions inputs, Transform target, GunHandler gunHandler, Transform camera) 
    {
        _playerView = playerView;
        _inputs = inputs;
        _target = target;
        _gunHandler = gunHandler;
        _camera = camera;
    }

    public List<State> GetStates()
    {
        State armedState = GetArmedState();
        State disarmedState = GetDisarmedState();

        var armedToDisarmed = new SwitchTransition(disarmedState);
        _gunHandler.Disarmed += armedToDisarmed.EnableCondition;

        var disarmedToArmed = new SwitchTransition(armedState);
        _gunHandler.Armed += disarmedToArmed.EnableCondition;

        disarmedState.AddTransition(disarmedToArmed);
        armedState.AddTransition(armedToDisarmed);

        return new List<State>() {disarmedState, armedState};
    }

    private State GetArmedState()
    {
        var runningState = new RunningState(_playerView);
        var sprintingState = new SprintingState(_playerView);
        var aimingState = new AimingState(_playerView);
        var jumpingState = new JumpingState(_playerView);

        var armedState = new StateMachine(new List<State>() { runningState, sprintingState, aimingState, jumpingState }, 0,
            new ArmedState(_playerView, _inputs, _target));

        var aimToRun = new SwitchTransition(runningState);
        _inputs.Aim.canceled += e => aimToRun.EnableCondition();

        var runToSprint = new SwitchTransition(sprintingState);
        _inputs.Sprint.started += e => runToSprint.EnableCondition();

        var runToJump = new SwitchTransition(jumpingState);
        _inputs.Jump.started += e => runToJump.EnableCondition();

        var runToAim = new SwitchTransition(aimingState);
        _inputs.Aim.started += e => runToAim.EnableCondition();

        var sprintToJump = new SwitchTransition(jumpingState);
        _inputs.Jump.started += e => sprintToJump.EnableCondition();

        var sprintToRun = new SwitchTransition(runningState);
        _inputs.Sprint.canceled += e => sprintToRun.EnableCondition();

        jumpingState.AddTransition(new ConditionTransition(runningState, _playerView.IsGrounded));
        aimingState.AddTransition(aimToRun);
        runningState.AddTransition(runToSprint);
        runningState.AddTransition(runToJump);
        runningState.AddTransition(runToAim);
        sprintingState.AddTransition(sprintToJump);
        sprintingState.AddTransition(sprintToRun);

        return armedState;
    }

    private State GetDisarmedState()
    {
        var runningState = new RunningState(_playerView);
        var sprintingState = new SprintingState(_playerView);
        var jumpingState = new JumpingState(_playerView);

        var disarmedState = new StateMachine(new List<State>() { runningState, sprintingState, jumpingState }, 0,
            new DisarmedState(_playerView, _inputs, _camera));

        var runToSprint = new SwitchTransition(sprintingState);
        _inputs.Sprint.started += e => runToSprint.EnableCondition();

        var runToJump = new SwitchTransition(jumpingState);
        _inputs.Jump.started += e => runToJump.EnableCondition();

        var sprintToJump = new SwitchTransition(jumpingState);
        _inputs.Jump.started += e => sprintToJump.EnableCondition();

        var sprintToRun = new SwitchTransition(runningState);
        _inputs.Sprint.canceled += e => sprintToRun.EnableCondition();

        jumpingState.AddTransition(new ConditionTransition(runningState, _playerView.IsGrounded));
        runningState.AddTransition(runToSprint);
        runningState.AddTransition(runToJump);
        sprintingState.AddTransition(sprintToJump);
        sprintingState.AddTransition(sprintToRun);

        return disarmedState;
    }
}
