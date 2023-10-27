using System.Collections;
using UnityEngine;
//using enemy;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _raySpawn;
    [SerializeField] private Transform _sh_spawn;
    [SerializeField] private Transform _secondHandPoint;
    [SerializeField] private MuzzleFlash _muzzleFlash;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _damage;
    [SerializeField] private int _shootDistance = 1000;
    [SerializeField] private float _reloadTime = 0.5f;
    [SerializeField] private float _lineDuration = 0.2f;
    [SerializeField] private LayerMask _mask = new();

    public Transform SecondPoint => _secondHandPoint;

    private float _lastShotTime;
    private RaycastHit _hit;
    private Ray _ray;
    private Transform _shell;

    private void Awake()
    {
        _lineRenderer.enabled = false;
    }

    public void TryFire() 
    {
        if (Time.time > _lastShotTime + _reloadTime)
        {
            _ray = new Ray(_raySpawn.position, _raySpawn.forward);
            if(Physics.Raycast(_ray, out _hit, _shootDistance, _mask))
            {
                Shoot();

                if (_hit.collider.TryGetComponent<IDamageable>(out var target))
                    target.TakeDamage(_damage);
            }
            
        }
    }

    private void Shoot()
    {
        StartCoroutine(ShowLaser(_hit.point));
        _muzzleFlash.Activate();
        _lastShotTime = Time.time;
        Debug.Log("Shoot");
        //SpawnShell();
    }

    private IEnumerator ShowLaser(Vector3 endPosition)
    {
        _lineRenderer.SetPosition(0, _raySpawn.position);
        _lineRenderer.SetPosition(1, endPosition);
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(_lineDuration);
        _lineRenderer.enabled = false;
        yield break;
    }

    //private void SpawnShell()
    //{
    //    _shell = PoolsContainer.Get(22).transform;
    //    _shell.SetPositionAndRotation(_sh_spawn.position, _sh_spawn.rotation);
    //    _shell.gameObject.SetActive(true);
    //}
}