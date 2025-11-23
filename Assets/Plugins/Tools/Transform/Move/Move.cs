using UnityEngine;

namespace GameBase.Tools.Transforms
{
    public static class Move
    {
        public static TaskResult DOMoveTo(this Transform transform, Vector3 position, float duration)
        {
            var task = new MoveTask(transform, position, duration);
            task.Result = new TaskResult();
            TransformTasks.Instance.AddTask(task);
            return task.Result;
        }

        public static TaskResult DOMoveVec(this Transform transform, Vector3 vec, float duration)
        {
            var task = new MoveTask(transform, transform.position + vec, duration);
            task.Result = new TaskResult();
            TransformTasks.Instance.AddTask(task);
            return task.Result;
        }
    }
}
