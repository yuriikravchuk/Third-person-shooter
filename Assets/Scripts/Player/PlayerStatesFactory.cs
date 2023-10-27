using stateMachine;
using System.Collections.Generic;
using UnityEngine;
using static GameInput;

public class PlayerStatesFactory
{
    private readonly PhysicsView _physicsView;
    private readonly AnimationView _animationView;
    private readonly PlayerActions _inputs;
    private readonly Transform _target;
    private readonly GunHandler _gunHandler;
    private readonly Transform _camera;
    public PlayerStatesFactory(PhysicsView physicsView, PlayerActions inputs, Transform target, GunHandler gunHandler, Transform camera) 
    {
        _physicsView = physicsView;
        _inputs = inputs;
        _target = target;
        _gunHandler = gunHandler;
        _camera = camera;
    }

    public List<State> GetStates()
    {
        var runningState = new FireableState(_gunHandler, _inputs, new RunningState(_animationView));
        var sprintingState = new HeadFolowingState(_gunHandler, new SprintingState(_animationView));
        var walkingState = new FireableState(_gunHandler, _inputs, new WalkingState(_animationView));
        var jumpingState = new FireableState(_gunHandler, _inputs, new JumpingState(_physicsView));

        var walkToRun = new SwitchTransition(runningState);
        _inputs.Aim.canceled += e => walkToRun.EnableCondition();

        var runToSprint = new SwitchTransition(sprintingState);
        _inputs.Sprint.started += e => runToSprint.EnableCondition();

        var runToJump = new SwitchTransition(jumpingState);
        _inputs.Jump.started += e => runToJump.EnableCondition();

        var runToWalk = new SwitchTransition(walkingState);
        _inputs.Aim.started += e => runToWalk.EnableCondition();

        var sprintToJump = new SwitchTransition(jumpingState);
        _inputs.Jump.started += e => sprintToJump.EnableCondition();

        var sprintToRun = new SwitchTransition(runningState);
        _inputs.Sprint.canceled += e => sprintToRun.EnableCondition();

        jumpingState.AddTransition(new ConditionTransition(runningState, _physicsView.IsGrounded));
        walkingState.AddTransition(walkToRun);
        runningState.AddTransition(runToSprint);
        runningState.AddTransition(runToJump);
        runningState.AddTransition(runToWalk);
        sprintingState.AddTransition(sprintToJump);
        sprintingState.AddTransition(sprintToRun);

        return new List<State>() { runningState, sprintingState, walkingState, jumpingState};
    }
}