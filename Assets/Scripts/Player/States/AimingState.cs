using stateMachine;

public class AimingState : State 
{
    private readonly PlayerView _playerView;

    public AimingState(PlayerView view) => _playerView = view;

    protected override void OnExit() { }

    protected override void OnEnter() => _playerView.Walk();

    protected override void OnUpdate() { }
}
