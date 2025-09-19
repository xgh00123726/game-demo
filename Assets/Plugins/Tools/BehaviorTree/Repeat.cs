namespace GameBase.Tools
{
    /// <summary>
    /// 重复一个节点n次
    /// </summary>
    public class Repeat : Decorator
    {
        public enum EndCondition
        {
            Over,
            Failure,
            Success,
        }
        private int _maxRunCount = 0;
        private EndCondition _endCondition = EndCondition.Over;
        
        public Repeat(int maxRunCount)
        {
            _maxRunCount = maxRunCount;
        }
        protected override Status OnUpdate()
        {
            for (int i = 0; i < _maxRunCount; ++i)
            {
                child.Tick();
                if (child.IsFailure && _endCondition == EndCondition.Failure) return Status.Failure;
                if (child.IsSuccess && _endCondition == EndCondition.Success) return Status.Success;
            }

            return Status.Success;
        }
    }
}
