using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private List<Gun> _guns;
    [SerializeField] private Rig _leftHandRig;
    [SerializeField] private RigBuilder _rigBuilder;
    [SerializeField] private TwoBoneIKConstraint _leftHandConstraint;

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
        _leftHandRig.weight = 0;
        DisArmed?.Invoke();
    }

    private void Arm(Gun gun)
    {
        _currentGun?.gameObject.SetActive(false);
        _currentGun = gun;
        _currentGun.gameObject.SetActive(true);
        _leftHandRig.weight = 1;
        _leftHandConstraint.data.target = SecondHandPoint; 
        _rigBuilder.Build();
        Armed?.Invoke();
    }
}
