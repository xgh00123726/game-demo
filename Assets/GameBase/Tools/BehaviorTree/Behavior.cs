namespace GameBase.Tools
{
    public class Behavior
    {
        public enum Status
        {
            Running,
            Failure,
            Success,
        }
        private Status status;

        public bool IsRunning => status == Status.Running;
        public bool IsFailure => status == Status.Failure;
        public bool IsSuccess => status == Status.Success;

        protected virtual void OnUpdate()
        {

        }
    }
}
