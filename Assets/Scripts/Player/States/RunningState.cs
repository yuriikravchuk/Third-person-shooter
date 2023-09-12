using stateMachine;

public class RunningState : HierarchicalState
{
    private readonly PlayerView _playerView;

    public RunningState(PlayerView playerView) => _playerView = playerView;

    protected override void OnEnter() => _playerView.Run();

    public override bool CanTransit(State state)
    {
        return state switch
        {
            AimingState => true,
            SprintingState => true,
            JumpingState => true,
            _ => false,
        };
    }
}
