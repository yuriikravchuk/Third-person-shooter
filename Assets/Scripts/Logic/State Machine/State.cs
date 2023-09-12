namespace stateMachine
{
    public abstract class State
    {
        public virtual void Enter() => OnEnter();

        public virtual void Update()
        {
            OnUpdate();
            TryTransit();
        }

        public virtual void Exit() => OnExit();

        public abstract bool CanTransit(State state);

        protected virtual void OnExit() { }
        protected virtual void OnEnter() { }
        protected virtual void OnUpdate() { }

        public virtual void TryTransit() { }

    }
}
