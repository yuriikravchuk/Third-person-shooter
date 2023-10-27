using stateMachine;

public class WalkingState : State 
{
    private readonly AnimationView _playerView;

    public WalkingState(AnimationView view) => _playerView = view;

    protected override void OnExit() { }

    protected override void OnEnter() => _playerView.Walk();

    protected override void OnUpdate() { }
}

