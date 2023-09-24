using System;
using System.Collections.Generic;
using System.Linq;

namespace stateMachine
{
    public class StateMachine : State
    {
        private readonly IReadOnlyList<State> _subStates;
        private readonly State _state;
        private State _currentSubState;

        public StateMachine(IReadOnlyList<State> states, int startSubStateIndex = 0, State state = null)
        {
            _state = state;
            _subStates = states;
            _currentSubState = states[startSubStateIndex];
            _currentSubState.Enter();

            foreach (State subState in _subStates)
                subState.StateSwitched += SwitchState;
        }

        protected sealed override void OnEnter() => _state?.Enter();

        protected sealed override void OnExit() => _state?.Exit();

        protected sealed override void OnUpdate()
        {
            _state?.Update();
            _currentSubState.Update();
        }

        private void SwitchState(State state)
        {
            if (_subStates.Contains(state) == false)
                throw new InvalidOperationException();

            _currentSubState.Exit();
            _currentSubState = state;
            _currentSubState.Enter();
        }
    }
}