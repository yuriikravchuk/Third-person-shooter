using stateMachine;

public class HeadFolowingState : State
{
    private readonly GunHandler _view;

    public HeadFolowingState(GunHandler view, State substate = null) : base(substate)
    {
        _view = view;
        view.Armed += OnArmed;
        view.Disarmed += OnDisarmed;
    }

    private void OnArmed()
    {
        if (Enabled)
            _view.SetHeadIKWeight(1);
    }

    private void OnDisarmed()
    {
        if (Enabled)
            _view.SetHeadIKWeight(0);
    }

    protected override void OnExit() => _view.SetHeadIKWeight(0);
}
