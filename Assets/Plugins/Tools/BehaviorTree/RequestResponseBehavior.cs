using System;

namespace GameBase.Tools
{
    public class RequestResponseBehavior : Behavior
    {
        private Func<Status> _action;
        public RequestResponseBehavior(Func<Status> RRAction)
        {
            _action = RRAction;
        }

        protected override Status OnUpdate()
        {
            if (_action == null) return Status.Failure;
            return _action.Invoke();
        }
    }
}
