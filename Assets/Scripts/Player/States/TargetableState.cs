using stateMachine;

public class TargetableState : State
{
    private readonly GunHandler _view;

    public TargetableState(GunHandler view, State substate = null) : base(new BodyFolowingState(view, substate))
    {
        _view = view;
        view.Armed += OnArmed;
        view.Disarmed += OnDisarmed;
    }

    private void OnArmed()
    {
        if(Enabled)
            _view.SetWeaponIKWeight(1);
    }

    private void OnDisarmed()
    {
        if (Enabled)
            _view.SetWeaponIKWeight(0);
    }

    protected override void OnExit() => _view.SetWeaponIKWeight(0);
}