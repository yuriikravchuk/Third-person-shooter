using stateMachine;
using System.Collections.Generic;
using UnityEngine;
using static GameInput;

public class Starter : MonoBehaviour
{
    [SerializeField] private Updater _updater;
    [SerializeField] private PlaySceneUI _gameUI;
    [SerializeField] private Settings _settings;
    [SerializeField] private PhysicsView _playerView;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private GunHandler _gunHandler;
    [SerializeField] private Transform _mouseTarget;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerHealthView _healthView;


    private void Awake()
    {
        var gameInput = new GameInput();
        gameInput.Enable();
        InitPlayer(gameInput.Player);
        var gameStateMachine = GetGameStateMachine(gameInput);
        gameStateMachine.Enter();
        var playerStateMachine = GetPlayerStateMachine(gameInput.Player);
        playerStateMachine.Enter();
        _updater.Init(new List<IUpdatable> { gameStateMachine, playerStateMachine });
    }

    private void InitPlayer(PlayerActions playerInput)
    {
        _healthView.Init(_playerHealth.Value);
        _playerHealth.ValueChanged += _healthView.SetValue;
        playerInput.View.performed += e => _playerCamera.Rotate(e.ReadValue<Vector2>());
        playerInput.First_Weapon.performed += e => _gunHandler.OnGunSelected(0);
        playerInput.Second_Weapon.performed += e => _gunHandler.OnGunSelected(1);
        playerInput.Third_Weapon.performed += e => _gunHandler.OnGunSelected(2);
    }

    private StateMachine GetPlayerStateMachine(PlayerActions playerInput)
    {
        var playerStatesFactory = new PlayerStatesFactory(_playerView, playerInput, _mouseTarget, _gunHandler, _playerCamera.RotationPoint);
        var states = playerStatesFactory.GetStates();
       return new StateMachine(states, 0);
    }

    private StateMachine GetGameStateMachine(GameInput playerInput)
    {
        var gameStatesFactory = new GameStatesFactory(playerInput, _settings, _gameUI);
        IReadOnlyList<State> gameStates = gameStatesFactory.GetStates();
        return new StateMachine(gameStates);
    }
}