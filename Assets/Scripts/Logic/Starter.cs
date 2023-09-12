using stateMachine;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private StateMachine _playerStateMachine;
    [SerializeField] private GunHandler _gunHandler;
    [SerializeField] private Transform _mouseTarget;

    private void Awake()
    {
        Cursor.visible = false;
        var playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.View.performed += e => _playerCamera.Rotate(e.ReadValue<Vector2>());
        _gunHandler.Armed += _playerStateMachine.TrySwitchState<ArmedState>;
        _gunHandler.DisArmed += _playerStateMachine.TrySwitchState<DisarmedState>;
        var playerStatesFactory = new PlayerStatesFactory(_playerView, playerInput, _mouseTarget, _gunHandler, _playerCamera.RotationPoint);
        var states = playerStatesFactory.GetStates();
        _playerStateMachine.Init(states, states[0]);

    }
}
