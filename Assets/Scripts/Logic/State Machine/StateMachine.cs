using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace stateMachine
{
    public class StateMachine : State
    {
        private readonly IReadOnlyList<State> _subStates;
        private State _currentSubState;
        private readonly int _startSubStateIndex;

        public StateMachine(IReadOnlyList<State> states, int startSubStateIndex = 0, State state = null) : base(state)
        {
            _subStates = states;
            _startSubStateIndex = startSubStateIndex;
            //SetState(_subStates[_startSubStateIndex]);

            foreach (State subState in _subStates)
                subState.StateSwitched += SwitchState;
        }

        protected sealed override void OnUpdate() => _currentSubState.Update();

        protected sealed override void OnEnter() => SetState(_subStates[_startSubStateIndex]);

        protected sealed override void OnExit() => _currentSubState.Exit();

        private void SwitchState(State state)
        {
            if (_subStates.Contains(state) == false)
                throw new InvalidOperationException();

            _currentSubState.Exit();
            SetState(state);
        }

        private void SetState(State state)
        {
            _currentSubState = state;
            _currentSubState.Enter();
            //Debug.Log(state.GetType());
        }
    }
}