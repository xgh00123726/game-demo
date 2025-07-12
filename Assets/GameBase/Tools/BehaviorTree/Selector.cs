namespace GameBase.Tools
{
    // 一个语句成功，就返回成功
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
