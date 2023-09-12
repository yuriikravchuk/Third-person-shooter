using UnityEngine;
//using enemy;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _raySpawn;
    [SerializeField] private Transform _sh_spawn;
    [SerializeField] private Transform _secondHandPoint;
    [SerializeField] private MuzzleFlash _muzzleFlash;
    [SerializeField] private int _damage;
    [SerializeField] private int _shootDistance = 1000;
    [SerializeField] private float _reloadTime = 0.5f;

    public Transform SecondPoint => _secondHandPoint;

    private float _lastShotTime;
    private RaycastHit _hit;
    private Ray _ray;
    private Transform _shell;

    public void TryShoot()
    {
        if (Time.time > _lastShotTime + _reloadTime)
        {
            _ray = new Ray(_raySpawn.position, _raySpawn.forward);
            if(Physics.Raycast(_ray, out _hit, _shootDistance))
            {
                if (_hit.collider.TryGetComponent<Enemy>(out var target))
                {
                    Shoot(target);
                    _lastShotTime = Time.time;
                }
            }

        }
    }

    private void Shoot(IDamageable target)
    {
        target.TryTakeDamage(_damage);
        //_muzzleFlash.Activate();
        //SpawnShell();
    }

    //private void SpawnShell()
    //{
    //    _shell = PoolsContainer.Get(22).transform;
    //    _shell.SetPositionAndRotation(_sh_spawn.position, _sh_spawn.rotation);
    //    _shell.gameObject.SetActive(true);
    //}
}