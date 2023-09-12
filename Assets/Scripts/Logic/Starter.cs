using stateMachine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private StateMachine _gameStateMachine;
    [SerializeField] private PlaySceneUI _gameUI;
    [SerializeField] private Settings _settings;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private StateMachine _playerStateMachine;
    [SerializeField] private GunHandler _gunHandler;
    [SerializeField] private Transform _mouseTarget;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerHealthView _healthView;
    [SerializeField] private int _maxPlayerHealth = 100;

    private void Awake()
    {
        var playerInput = new PlayerInput();
        playerInput.Enable();
        var gameStates = new List<State>() { new PlayState(_gameUI, playerInput), new MenuState(_gameUI) };
        _gameStateMachine.Init(gameStates, gameStates[0]);
        playerInput.Global.Pause.performed += e => _gameStateMachine.TrySwitchState<MenuState>();
        _settings.ResumeButtonClicked += _gameStateMachine.TrySwitchState<PlayState>;
        playerInput.Player.View.performed += e => _playerCamera.Rotate(e.ReadValue<Vector2>());
        _gunHandler.Armed += _playerStateMachine.TrySwitchState<ArmedState>;
        _gunHandler.DisArmed += _playerStateMachine.TrySwitchState<DisarmedState>;
        var playerStatesFactory = new PlayerStatesFactory(_playerView, playerInput, _mouseTarget, _gunHandler, _playerCamera.RotationPoint);
        var states = playerStatesFactory.GetStates();
        _playerStateMachine.Init(states, states[0]);
        var health = new Health(_maxPlayerHealth);
        _healthView.Init(_maxPlayerHealth);
        health.ValueChanged += _healthView.OnValueChanged;
        _player.Init(health);
    }
}
