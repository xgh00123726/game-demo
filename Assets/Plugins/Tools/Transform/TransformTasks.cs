namespace GameBase.Tools.Transforms
{
    internal class TransformTasks : SingletonInstance<TransformTasks>
    {
        private EntitySys<ITask> _tasks;
        public TransformTasks()
        {
            _tasks = new EntitySys<ITask>(new ListContainer<ITask>())
            {
                UpdateAction = UpdateTask,
            };
        }

        public void AddTask(ITask task)
        {
            _tasks.AddEntity(task);
        }

        protected void UpdateTask(ITask task)
        {
            task.Update();
        }

        protected override void Update()
        {
            foreach (var task in _tasks.Entities)
            {
                if (task.IsOver)
                {
                    _tasks.RemoveEntity(task);
                    task.Result.ThenAction?.Invoke();
                }
            }

            _tasks.Iterate();
        }
    }
}
