using System.Collections.Generic;

namespace GameBase.Tools
{
    // 一个语句失败，就返回失败
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
