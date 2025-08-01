namespace GameBase.Tools
{
    public class RequestResponseBehavior : Behavior
    {
        public delegate Status RRAction();
        private RRAction _action;
        public RequestResponseBehavior(RRAction action)
        {
            _action = action;
        }

        protected override Status OnUpdate()
        {
            if (_action == null) return Status.Failure;
            return _action.Invoke();
        }
    }
}
