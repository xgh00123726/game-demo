namespace GameBase.Tools
{
    public class ConditionBehavior : Behavior
    {
        public delegate bool ConditionAction();
        private ConditionAction _conditionAction;
        public ConditionBehavior(ConditionAction conditionAction)
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
