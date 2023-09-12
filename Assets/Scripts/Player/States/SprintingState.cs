using stateMachine;

public class SprintingState : HierarchicalState
{
    private readonly PlayerView _playerView;

    public SprintingState(PlayerView playerView) => _playerView = playerView;

    protected override void OnEnter() => _playerView.Sprint();

    public override bool CanTransit(State state)
    {
        return state switch
        {
            RunningState => true,
            JumpingState => true,
            _ => false,
        };
    }
}
