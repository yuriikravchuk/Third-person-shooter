using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private List<Gun> _guns;
    [SerializeField] private MultiAimConstraint _bodyIK;
    [SerializeField] private MultiAimConstraint _headIK;
    [SerializeField] private MultiAimConstraint _weaponIK;
    [SerializeField] private TwoBoneIKConstraint _leftHandIK;

    public event Action Armed
    {
        add => _armed += value;
        remove => _armed -= value;
    }

    public event Action Disarmed
    {
        add => _disarmed += value;
        remove => _disarmed -= value;
    }

    private event Action _armed, _disarmed;

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

    public void TryFire() => _currentGun?.TryFire();

    public void SetBodyIKWeight(float weight) => _bodyIK.weight = weight;

    public void SetHeadIKWeight(float weight) => _headIK.weight = weight;

    public void SetWeaponIKWeight(float weight)
    {
        _weaponIK.weight = weight;
        _leftHandIK.weight = weight;
    }

    private void Disarm()
    {
        _currentGun.gameObject.SetActive(false);
        _currentGun = null;
        _disarmed?.Invoke();
    }

    private void Arm(Gun gun)
    {
        _currentGun?.gameObject.SetActive(false);
        _currentGun = gun;
        _currentGun.gameObject.SetActive(true);
        _armed?.Invoke();
    }
}