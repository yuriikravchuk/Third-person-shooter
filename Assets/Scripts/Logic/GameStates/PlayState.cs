using stateMachine;
using UnityEngine;

public class PlayState : State
{
    private readonly PlaySceneUI _playUI;
    private readonly GameInput _playerInput;

    public PlayState(PlaySceneUI playUI, GameInput playerInput)
    {
        _playUI = playUI;
        _playerInput = playerInput;
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

    protected override void OnUpdate()
    {
        
    }
}