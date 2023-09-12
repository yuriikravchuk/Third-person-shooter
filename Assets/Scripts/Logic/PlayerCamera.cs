﻿using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _smooth;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _rotationPoint;
    [SerializeField] private GameObject _cube;
    [SerializeField] private LayerMask _mask = new();
    [SerializeField] private float _sensivity = 1f;

    public Transform RotationPoint => _rotationPoint;

    private Vector3 _velocity, _targetRotation;

    private void Awake()
    {
        _targetRotation = _rotationPoint.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _player.position, ref _velocity, _smooth);
        _rotationPoint.localRotation = Quaternion.Euler(_targetRotation);
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, _mask))
            _cube.transform.position = hit.point;
    }

    public void Rotate(Vector2 delta)
    {
        _targetRotation.y += delta.x * _sensivity;
        _targetRotation.x += delta.y * _sensivity;
        _targetRotation.x = Mathf.Clamp(_targetRotation.x, -40, 65);
    }
}