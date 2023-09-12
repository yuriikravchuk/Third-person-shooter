using stateMachine;
using UnityEngine;

public class PlayState : State
{
    private readonly PlaySceneUI _playUI;
    private readonly PlayerInput _playerInput;

    public PlayState(PlaySceneUI playUI, PlayerInput playerInput)
    {
        _playUI = playUI;
        _playerInput = playerInput;
    }

    public override bool CanTransit(State state)
    {
        return state is MenuState;
    }

    protected override void OnEnter()
    {
        _playUI.ShowPlayUI();
        Cursor.visible = false;
        _playerInput.Player.Enable();
    }

    protected override void OnExit()
    {
        _playUI.HidePlayUI();
        Cursor.visible = true;
        _playerInput.Player.Disable();
    }
}