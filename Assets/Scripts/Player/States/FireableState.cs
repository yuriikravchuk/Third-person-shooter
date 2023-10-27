using stateMachine;
using static GameInput;

public class FireableState : State
{
    private readonly GunHandler _gunHandler;
    private bool _shootButtonPressed = false;
    private bool _isArmed;

    public FireableState(GunHandler gunHandler, PlayerActions playerInput, State substate = null) : base(new TargetableState(gunHandler, substate))
    {
        _gunHandler = gunHandler;
        playerInput.Shoot.started += e => _shootButtonPressed = true;
        playerInput.Shoot.canceled += e => _shootButtonPressed = false;
        gunHandler.Armed += () => _isArmed = true;
        gunHandler.Disarmed += () => _isArmed = false;
    }

    protected override void OnUpdate()
    {
        if (_shootButtonPressed & _isArmed)
            _gunHandler.TryFire();
    }
}