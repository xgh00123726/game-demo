using System;

namespace GameBase.Tools
{
    public class SuccessFireForgetBehavior : Behavior
    {
        private Action _action;
        public SuccessFireForgetBehavior(Action action)
        {
            _action = action;
        }
        protected override Status OnUpdate()
        {
            _action?.Invoke();
            return Status.Success;
        }
    }
}