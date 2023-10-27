using stateMachine;

public class BodyFolowingState : State
{
    private readonly GunHandler _view;

    public BodyFolowingState(GunHandler view, State substate = null) : base(new HeadFolowingState(view, substate))
    {
        _view = view;
        view.Armed += OnArmed;
        view.Disarmed += OnDisarmed;
    }
    private void OnArmed()
    {
        if (Enabled)
            _view.SetBodyIKWeight(1);
    }

    private void OnDisarmed()
    {
        if (Enabled)
            _view.SetBodyIKWeight(0);
    }

    protected override void OnExit() => _view.SetBodyIKWeight(0);
}
