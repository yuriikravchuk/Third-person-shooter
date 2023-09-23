using stateMachine;
using UnityEngine;
using static GameInput;

public class ArmedState : State // rotate towards camera
{
    private readonly PlayerView _playerView;
    private readonly PlayerActions _playerInput;
    //private readonly GunHandler _gunHandler;
    private readonly Transform _targetTransform;
    private Vector2 _moveVector;
    //private bool ShootButtonPressed = false;

    public ArmedState(GunHandler gunHandler,PlayerView playerView, PlayerActions playerInput, Transform targetTransform)
    {
        _playerView = playerView;
        _playerInput = playerInput;
        _targetTransform = targetTransform;
        //_gunHandler = gunHandler;
        //_playerInput.Aim.Disable();
        //_playerInput.Shoot.started += e => ShootButtonPressed = true;
        //_playerInput.Shoot.canceled += e => ShootButtonPressed = false;
        //_playerInput.First_Weapon.performed += e => _gunHandler.OnGunSelected(0);
        //_playerInput.Second_Weapon.performed += e => _gunHandler.OnGunSelected(1);
        //_playerInput.Third_Weapon.performed += e => _gunHandler.OnGunSelected(2);
    }

    protected override void OnEnter()
    {
        _playerView.SetArmed();
        //_playerInput.Aim.Enable();
        _playerInput.Move.performed += e => _moveVector = e.ReadValue<Vector2>();
        _playerInput.Move.canceled += e => _moveVector = Vector2.zero;
    }

    protected override void OnExit()
    {
        _playerView.SetDisarmed();
        //_playerInput.Aim.Disable();
    }

    protected override void OnUpdate()
    {
        _playerView.Rotate(_targetTransform.position - _playerView.transform.position);
        _playerView.Move(_moveVector);
    }
}