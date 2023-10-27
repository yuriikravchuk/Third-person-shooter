using System;
using System.Collections.Generic;

namespace stateMachine
{
    public abstract class State : IUpdatable
    {
        public event Action<State> StateSwitched
        {
            add => _stateSwitched += value;
            remove => _stateSwitched -= value;
        }

        protected readonly List<Transition> Transitions;
        protected bool Enabled { get; private set; }

        private readonly State _subState;
        private event Action<State> _stateSwitched;

        public State(State substate = null)
        {
            Transitions = new List<Transition>();
            _subState = substate;
        }

        public void Update()
        {
            TryTransit();
            OnUpdate();
            _subState?.Update();
        }

        public void Enter()
        {
            foreach (var transition in Transitions)
                transition.Enable();

            OnEnter();
            _subState?.Enter();
            Enabled = true;
        }

        public void Exit()
        {
            foreach (var transition in Transitions)
                transition.Disable();

            OnExit();
            _subState?.Exit();
            Enabled = false;
        }

        public void TryTransit()
        {
            foreach (var transition in Transitions)
            {
                if (transition.CanTransit)
                    _stateSwitched.Invoke(transition.Target);
            }
        }

        public void AddTransition(Transition transition) => Transitions.Add(transition);

        protected virtual void OnExit() { }
        protected virtual void OnEnter() { }
        protected virtual void OnUpdate() { }
    }
}
