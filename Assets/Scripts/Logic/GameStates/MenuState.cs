using stateMachine;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MenuState : State
{
    private readonly PlaySceneUI _ui;
    private readonly Action<CallbackContext> _onPauseButtonPressed;

    public MenuState(PlaySceneUI menuUI)
    {
        _ui = menuUI;
    }

    public override bool CanTransit(State state)
    {
        return state is PlayState;
    }

    protected override void OnEnter()
    {
        _ui.ShowSettings();
    }

    protected override void OnExit()
    {
        _ui.HideSettings();
    }
}
