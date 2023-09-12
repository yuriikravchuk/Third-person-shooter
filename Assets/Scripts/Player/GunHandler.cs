using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private List<Gun> _guns;

    public event Action Armed;
    public event Action DisArmed;

    public Transform SecondHandPoint => _currentGun?.SecondPoint;

    private Gun _currentGun;

    public void OnGunSelected(int index)
    {
        Gun nextGun = _guns[index] ?? throw new NullReferenceException();

        if (nextGun == _currentGun)
            Disarm();
        else
            Arm(nextGun);

    }

    public void TryFire()
    {
        if (_currentGun == null) return;

        _currentGun.TryShoot();
    }

    private void Disarm()
    {
        _currentGun.gameObject.SetActive(false);
        _currentGun = null;
        DisArmed?.Invoke();
    }

    private void Arm(Gun gun)
    {
        _currentGun?.gameObject.SetActive(false);
        _currentGun = gun;
        _currentGun.gameObject.SetActive(true);
        Armed?.Invoke();
    }
}
