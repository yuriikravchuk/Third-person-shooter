using System;

namespace stateMachine
{
    public abstract class Transition
    {
        public readonly State Target;
        public abstract bool CanTransit { get; }

        protected bool Enabled { get; private set; }
        protected Action OnEnable, OnDisable;

        public Transition(State target) => Target = target;

        public void Enable()
        {
            Enabled = true;
            OnEnable?.Invoke();
        }

        public void Disable()
        {
            Enabled = false;
            OnDisable?.Invoke();
        }
    }

    public class ConditionTransition : Transition
    {
        public override bool CanTransit => _condition == null || _condition();

        private readonly Func<bool> _condition;

        public ConditionTransition(State target, Func<bool> condition = null) : base(target)
            => _condition = condition;
    }

    public class SwitchTransition : Transition
    {
        public override bool CanTransit => _condition;

        private bool _condition = false;

        public SwitchTransition(State target) : base(target) => OnEnable += DisableCondition;

        public void EnableCondition() => _condition = true;

        public void DisableCondition() => _condition = false;
    }
}
