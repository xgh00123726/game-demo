using System;

namespace GameBase.Tools
{
    public class NConditionBehavior : Behavior
    {
        private Func<bool> _conditionAction;
        public NConditionBehavior(Func<bool> conditionAction)
        {
            _conditionAction = conditionAction;
        }
        protected override Status OnUpdate()
        {
            if (_conditionAction?.Invoke() == true) return Status.Failure;
            return Status.Success;
        }
    }
}
