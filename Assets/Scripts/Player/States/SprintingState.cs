using stateMachine;

public class SprintingState : State
{
    private readonly PlayerView _playerView;

    public SprintingState(PlayerView playerView) => _playerView = playerView;

    protected override void OnExit() { }

    protected override void OnEnter() => _playerView.Sprint();

    protected override void OnUpdate() { }
}
