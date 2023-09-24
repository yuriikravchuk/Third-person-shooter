using stateMachine;

public class RunningState : State
{
    private readonly PlayerView _playerView;

    public RunningState(PlayerView playerView) => _playerView = playerView;

    protected override void OnExit() { }

    protected override void OnEnter() => _playerView.Run();

    protected override void OnUpdate() { }
}
