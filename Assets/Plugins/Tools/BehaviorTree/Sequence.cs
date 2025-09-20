using System.Collections.Generic;

namespace GameBase.Tools
{
    /// <summary>
    /// 所有语句都成功时才返回成功，and逻辑
    /// </summary>
    public class Sequence : Composite
    {
        protected override Status OnUpdate()
        {
            foreach (var child in children)
            {
                child.Tick();
                if (child.IsFailure) return Status.Failure;
                if (child.IsRunning) return Status.Running;
            }
            return Status.Success;
        }
    }
}
