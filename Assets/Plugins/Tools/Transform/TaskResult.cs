using System;

namespace GameBase.Tools.Transforms
{
    public class TaskResult
    {
        internal Action ThenAction { get; private set; }
        public void Then(Action action)
        {
            ThenAction = action;
        }
    }
}
