using stateMachine;
public class JumpingState : State
{
    private readonly PlayerView _playerView;

    public JumpingState(PlayerView playerView) => _playerView = playerView;

    protected override void OnEnter() => _playerView.AddJumpForce();

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {
        
    }
}