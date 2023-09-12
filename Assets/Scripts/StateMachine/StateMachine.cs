using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace stateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private IReadOnlyList<State> _states;
        private State _currentState;

        public void Init(IReadOnlyList<State> states, State startState)
        {
            _states = states;
            _currentState = startState;
            _currentState.Enter();
        }

        private void Update() => _currentState.Update();

        public void TrySwitchState<T>()
        {
            State nextState = FindState<T>();
            if (_currentState.CanTransit(nextState))
                SwitchState(nextState);

            Debug.Log(typeof(T));
        }

        private void SwitchState(State state)
        {
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        private State FindState<T>()
            => _states.FirstOrDefault(x => x is T) ?? throw new InvalidOperationException();
    }
}