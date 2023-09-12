using stateMachine;

public class AimingState : HierarchicalState 
{
    private readonly PlayerView _playerView;

    public AimingState(PlayerView view) => _playerView = view;

    protected override void OnEnter() => _playerView.Walk();

    public override bool CanTransit(State state)
    {
        return state switch
        {
            RunningState => true,
            _ => false,
        };
    }
}
