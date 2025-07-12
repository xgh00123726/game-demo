namespace GameBase.Tools
{
    public class Inverter : Decorator
    {
        protected override Status OnUpdate()
        {
            child.Tick();
            if (child.IsSuccess) return Status.Failure;
            if (child.IsFailure) return Status.Success;
            return Status.Running;
        }
    }
}
