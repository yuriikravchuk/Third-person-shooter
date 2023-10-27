using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationView : MonoBehaviour
{
    [SerializeField] private float _animationSmoothness = 0.05f;

    private Animator _animator;
    private Vector2 _moveVector = Vector2.zero;
    private float _moveSpeed;
    private const float
        WALKING_SPEED = 0.33f,
        RUNNING_SPEED = 0.66f,
        SPRINTING_SPEED = 1f,
        SPEED_ROTATION = 0.2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveSpeed = RUNNING_SPEED;
    }


    public void Walk() => _moveSpeed = WALKING_SPEED;

    public void Run() => _moveSpeed = RUNNING_SPEED;

    public void Sprint() => _moveSpeed = SPRINTING_SPEED;

    public void EndJump() => _animator.SetBool("jump", false);

    public void SetArmed() => _animator.SetBool("armed", true);

    public void SetDisarmed() => _animator.SetBool("armed", false);

    public void Jump() => _animator.SetBool("jump", true);

    private void UpdateMovement()
    {
        _animator.SetFloat("X", _moveVector.x * _moveSpeed, _animationSmoothness, Time.deltaTime);
        _animator.SetFloat("Y", _moveVector.y * _moveSpeed, _animationSmoothness, Time.deltaTime);
        _animator.SetFloat("moving_magnitude", _moveVector.magnitude * _moveSpeed, _animationSmoothness, Time.deltaTime);
    }
}
