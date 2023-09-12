using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _smooth;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _rotationPoint;
    [SerializeField] private GameObject _cube;
    [SerializeField] private LayerMask _mask = new();
    private float _sensivity = 0.5f;

    public Transform RotationPoint => _rotationPoint;

    private Vector3 _velocity, _targetRotation;
    private bool _invertYAxis = true;

    private void Awake() => _targetRotation = _rotationPoint.rotation.eulerAngles;

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _player.position, ref _velocity, _smooth);
        _rotationPoint.localRotation = Quaternion.Euler(_targetRotation);
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, _mask))
            _cube.transform.position = hit.point;
    }

    public void SetSensivity(float value) => _sensivity = value;

    public void InvertYAxisChanged(bool value) => _invertYAxis = value;

    public void Rotate(Vector2 delta)
    {
        _targetRotation.y += delta.x * _sensivity;
        _targetRotation.x += delta.y * (_invertYAxis ? -_sensivity : _sensivity); ;
        _targetRotation.x = Mathf.Clamp(_targetRotation.x, -40, 65);
    }
}