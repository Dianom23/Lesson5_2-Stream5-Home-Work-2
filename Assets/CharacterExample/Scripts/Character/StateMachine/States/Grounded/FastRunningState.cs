public class FastRunningState : GroundedState
{
    private readonly FastRunningStateConfig _config;

    public FastRunningState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _config = character.Config.FastRunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        View.StartFastRunning();
        Data.Speed = _config.Speed;
    }

    public override void Exit()
    {
        base.Exit();

        View.StopFastRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();

        if (IsRunKeyHolded() == false)
            StateSwitcher.SwitchState<WalkingState>();

        if (IsRunKeyHolded() && IsFastRunKeyHolded() == false)
            StateSwitcher.SwitchState<RunningState>();
    }
}