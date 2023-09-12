using System;
using System.Collections.Generic;

namespace stateMachine
{
    public abstract class HierarchicalState : State
    {
        protected HierarchicalState SubState { get; private set; }
        protected List<HierarchicalState> SubStates;
        protected HierarchicalState SuperState;

        public void TrySetSubState<T>() where T : HierarchicalState
        {
            HierarchicalState nextSubState = SubStates.Find(x => x is T) ?? throw new InvalidOperationException();

            if (SubState != null && SubState.CanTransit(nextSubState) == false) 
                return;

            SetSubState(nextSubState);
        }

        public void SetSuperState(HierarchicalState superState) => SuperState = superState;

        public sealed override void Enter()
        {
            base.Enter();
            if (SubStates?.Count > 0)
            {
                foreach (var subState in SubStates)
                    subState.SetSuperState(this);

                InitSubState();
            }
        }

        public sealed override void Update()
        {
            base.Update();
            SubState?.Update();
        }

        public sealed override void Exit()
        {
            base.Exit();
            SubState?.Exit();
        }

        protected virtual void InitSubState() { }


        
        protected virtual void OnSubStateChanged(HierarchicalState state) { } 

        private void SetSubState(HierarchicalState state)
        {
            OnSubStateChanged(state);
            SubState = state;
            SubState.SetSuperState(this);
            SubState.Enter();
        }
    }
}
