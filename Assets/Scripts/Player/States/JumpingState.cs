using stateMachine;
public class JumpingState : State
{
    private readonly PhysicsView _playerView;

    public JumpingState(PhysicsView playerView) => _playerView = playerView;

    protected override void OnEnter() => _playerView.AddJumpForce();

    protected override void OnUpdate() { }

    protected override void OnExit() { }
}