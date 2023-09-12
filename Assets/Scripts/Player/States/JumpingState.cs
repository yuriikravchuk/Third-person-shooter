using stateMachine;

public class JumpingState : HierarchicalState
{
    private readonly PlayerView _playerView;

    public JumpingState(PlayerView playerView) => _playerView = playerView;

    protected override void OnEnter()
    {
        _playerView.AddJumpForce();
    }

    public override bool CanTransit(State state)
    {
        return state switch
        {
            RunningState => true,
            _ => false,
        };
    }

    public override void TryTransit()
    {
        if (_playerView.IsGrounded())
            SuperState.TrySetSubState<RunningState>();
    }
}