namespace GameBase.Tools
{
    // 一个语句成功，就返回成功
    /// <summary>
    /// 一个语句成功就返回成功，or逻辑
    /// </summary>
    public class Selector : Composite
    {
        protected override Status OnUpdate()
        {
            foreach (var child in children)
            {
                child.Tick();
                if (child.IsSuccess) return Status.Success;
                if (child.IsRunning) return Status.Running;
            }
            return Status.Failure;
        }
    }
}
