using stateMachine;

public class SprintingState : State
{
    private readonly AnimationView _playerView;

    public SprintingState(AnimationView playerView) => _playerView = playerView;

    protected override void OnExit() { }

    protected override void OnEnter() => _playerView.Sprint();

    protected override void OnUpdate() { }
}
