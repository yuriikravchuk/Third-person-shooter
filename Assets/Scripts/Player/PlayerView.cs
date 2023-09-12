using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 300;
    [SerializeField] private float _animationSmoothness = 0.05f;
    [SerializeField] private Rig _mainRig;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Vector2 _moveVector = Vector2.zero;
    private Vector3 _targetRotation;
    private float _moveSpeed;
    private const float WALKING_SPEED = 0.33f, RUNNING_SPEED = 0.66f, SPRINTING_SPEED = 1f;
    private const float SPEED_ROTATION = .2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        
        _targetRotation = transform.localRotation.eulerAngles;
        _moveSpeed = RUNNING_SPEED;
    }

    private void FixedUpdate()
    {
        SetRotation(_targetRotation);
        UpdateMovement();
    }

    public void SetRotation(Vector3 rotateVector)
    {
        var direction = Vector3.RotateTowards(transform.forward, rotateVector, SPEED_ROTATION, 0);
        direction.y = 0;
        transform.localRotation = Quaternion.LookRotation(direction);
    }

    public void Move(Vector2 moveVector) => _moveVector = moveVector.normalized;

    public void Walk() => _moveSpeed = WALKING_SPEED;

    public void Run() => _moveSpeed = RUNNING_SPEED;

    public void Sprint() => _moveSpeed = SPRINTING_SPEED;

    public void Jump() => _animator.SetBool("jump", true);

    public void AddJumpForce() => _rigidbody.AddForce(_moveVector.x, _jumpForce, _moveVector.y);

    public void Rotate(Vector3 direction) => _targetRotation = direction;

    public void EndJump() => _animator.SetBool("jump", false);

    public void Aim() => _mainRig.weight = 1f;

    public void StopAiming() => _mainRig.weight = 0f;

    public void SetArmed() => _animator.SetBool("armed", true);

    public void SetDisarmed() => _animator.SetBool("armed", false);

    private void UpdateMovement()
    {
        _animator.SetFloat("X", _moveVector.x * _moveSpeed, _animationSmoothness, Time.deltaTime);
        _animator.SetFloat("Y", _moveVector.y * _moveSpeed, _animationSmoothness, Time.deltaTime);
        _animator.SetFloat("moving_magnitude", _moveVector.magnitude * _moveSpeed, _animationSmoothness, Time.deltaTime);
    }

    public bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, 0.1f);
}
