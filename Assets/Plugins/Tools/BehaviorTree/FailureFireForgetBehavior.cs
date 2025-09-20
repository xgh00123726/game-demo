using System;

namespace GameBase.Tools
{
    public class FailureFireForgetBehavior : Behavior
    {
        private Action _action;
        public FailureFireForgetBehavior(Action action)
        {
            _action = action;
        }
        protected override Status OnUpdate()
        {
            _action?.Invoke();
            return Status.Failure;
        }
    }
}