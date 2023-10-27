using stateMachine;
using System.Collections.Generic;
using UnityEngine;

public class GameStatesFactory
{
    private readonly GameInput _playerInput;
    private readonly Settings _settings;
    private readonly PlaySceneUI _gameUI;

    public GameStatesFactory(GameInput playerInput, Settings settings, PlaySceneUI gameUI)
    {
        _playerInput = playerInput;
        _settings = settings;
        _gameUI = gameUI;
    }

    public IReadOnlyList<State> GetStates()
    {
        var playState = new PlayState(_gameUI, _playerInput);
        var menuState = new MenuState(_gameUI);

        var playToMenu = new SwitchTransition(menuState);
        _playerInput.Global.Pause.performed += e => playToMenu.EnableCondition();
        _playerInput.Global.Pause.performed += e => Debug.Log("Pause");
        var menuToPlay = new SwitchTransition(playState);
        _settings.ResumeButtonClicked += menuToPlay.EnableCondition;

        playState.AddTransition(playToMenu);
        menuState.AddTransition(menuToPlay);

        return new List<State>() { playState, menuState };
    }
}