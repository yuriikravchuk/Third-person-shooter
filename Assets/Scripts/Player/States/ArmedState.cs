using stateMachine;
using UnityEngine;
using static GameInput;

//public class ArmedState : State
//{
//    private readonly PhysicsView _playerView;
//    private readonly Transform _targetTransform;
//    private Vector2 _moveVector;
//    //private bool ShootButtonPressed = false;

//    public ArmedState(PhysicsView playerView, PlayerActions playerInput, Transform targetTransform)
//    {
//        _playerView = playerView;
//        _targetTransform = targetTransform;
//        playerInput.Move.performed += e => _moveVector = e.ReadValue<Vector2>();
//        playerInput.Move.canceled += e => _moveVector = Vector2.zero;
//        //_playerInput.Shoot.started += e => ShootButtonPressed = true;
//        //_playerInput.Shoot.canceled += e => ShootButtonPressed = false;
//    }

//    protected override void OnEnter() => _playerView.SetArmed();

//    protected override void OnUpdate()
//    {
//        _playerView.Rotate(_targetTransform.position - _playerView.transform.position);
//        _playerView.Move(_moveVector);
//    }

//    protected override void OnExit() { }
//}