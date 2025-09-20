namespace GameBase.Tools
{
    public abstract class Behavior
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

        // 
        /// <summary>
        /// <list type="bullet">
        /// <item>节点每次tick都会运行OnUpdate，返回值用于通知当前节点状态</item>
        /// <item>节点状态是private的，只会在tick函数中通过OnUpdate的返回值修改</item>
        /// <item>节点状态可以通过IsSuccess，IsFailure，IsRunning获取</item>
        /// </list></summary>
        /// <returns>节点当前状态</returns>
        protected abstract Status OnUpdate();
        protected virtual void OnInitialize() { }
        protected virtual void OnTerminate() { }
        public virtual void AddChild(Behavior child) { }
        public void Tick()
        {
            // 如果进入状态时，节点未运行，则表示节点第一次进入
            if (!IsRunning)
            {
                OnInitialize();
            }
            status = OnUpdate();
            // 如果退出状态时（执行完update之后），节点未运行，则表示节点完成一次运行
            if (!IsRunning)
            {
                OnTerminate();
            }
        }
    }
}
