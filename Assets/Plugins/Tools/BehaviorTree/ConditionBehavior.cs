using System;

namespace GameBase.Tools
{
    public class ConditionBehavior : Behavior
    {
        private Func<bool> _conditionAction;
        public ConditionBehavior(Func<bool> conditionAction)
        {
            _conditionAction = conditionAction;
        }
        protected override Status OnUpdate()
        {
            if (_conditionAction?.Invoke() == true) return Status.Success;
            return Status.Failure;
        }
    }
}
