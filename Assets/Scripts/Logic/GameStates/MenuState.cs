using stateMachine;
using System;
using static UnityEngine.InputSystem.InputAction;

public class MenuState : State
{
    private readonly PlaySceneUI _ui;

    public MenuState(PlaySceneUI menuUI) => _ui = menuUI;

    protected override void OnExit() => _ui.HideSettings();
    protected override void OnEnter() => _ui.ShowSettings();

    protected override void OnUpdate() { }
}
