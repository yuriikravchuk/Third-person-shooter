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

        private event Action<State> _stateSwitched;

        public State() => Transitions = new List<Transition>();

        public void Update()
        {
            TryTransit();
            OnUpdate();
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

        public void Enter()
        {
            foreach (var transition in Transitions)
                transition.Enable();

            OnEnter();
        }

        public void Exit()
        {
            foreach (var transition in Transitions)
                transition.Disable();

            OnExit();
        }

        protected abstract void OnExit();
        protected abstract void OnEnter();
        protected abstract void OnUpdate();
    }
}
