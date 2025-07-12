using System;

namespace GameBase.Tools
{
    public class FireForgetBehavior : Behavior
    {
        private Action _action;
        public FireForgetBehavior(Action action)
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