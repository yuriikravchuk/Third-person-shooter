using stateMachine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static GameInput;

public class Starter : MonoBehaviour
{
    [SerializeField] private Updater _updater;
    [SerializeField] private PlaySceneUI _gameUI;
    [SerializeField] private Settings _settings;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private GunHandler _gunHandler;
    [SerializeField] private Transform _mouseTarget;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerHealthView _healthView;
    [SerializeField] private int _maxPlayerHealth = 100;


    private void Awake()
    {
        var gameInput = new GameInput();
        gameInput.Enable();
        var gameStateMachine = GetGameStateMachine(gameInput);
        var playerStateMachine = GetPlayerStateMachine(gameInput.Player);
        InitPlayer(gameInput);

        _updater.Init(new List<IUpdatable> {gameStateMachine, playerStateMachine });
    }

    private void InitPlayer(GameInput playerInput)
    {
        var health = new Health(_maxPlayerHealth);
        _healthView.Init(_maxPlayerHealth);
        health.ValueChanged += _healthView.OnValueChanged;
        playerInput.Player.View.performed += e => _playerCamera.Rotate(e.ReadValue<Vector2>());
        _player.Init(health);
    }

    private StateMachine GetPlayerStateMachine(PlayerActions playerInput)
    {
        var playerStatesFactory = new PlayerStatesFactory(_playerView, playerInput, _mouseTarget, _gunHandler, _playerCamera.RotationPoint);
        var states = playerStatesFactory.GetStates();
       return new StateMachine(states);
    }

    private StateMachine GetGameStateMachine(GameInput playerInput)
    {
        var playState = new PlayState(_gameUI, playerInput);
        var menuState = new MenuState(_gameUI);

        var playToMenu = new SwitchTransition(menuState);
        playerInput.Global.Pause.performed += e => playToMenu.EnableCondition();
        playerInput.Global.Pause.performed += e => Debug.Log("Pause");
        var menuToPlay = new SwitchTransition(playState);
        _settings.ResumeButtonClicked += menuToPlay.EnableCondition;

        playState.AddTransition(playToMenu);
        menuState.AddTransition(menuToPlay);

        var gameStates = new List<State>() { playState, menuState };
        return new StateMachine(gameStates);

    }
}
