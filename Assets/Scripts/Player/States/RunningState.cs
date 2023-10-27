using stateMachine;

public class RunningState : State
{
    private readonly AnimationView _playerView;

    public RunningState(AnimationView playerView) => _playerView = playerView;

    protected override void OnExit() { }

    protected override void OnEnter() => _playerView.Run();

    protected override void OnUpdate() { }
}
