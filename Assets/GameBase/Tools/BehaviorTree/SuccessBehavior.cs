namespace GameBase.Tools
{
    public class SuccessBehavior : Behavior
    {
        protected override Status OnUpdate()
        {
            return Status.Success;
        }
    }
}
