using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    internal class TimerTaskQueue
    {
        internal static List<TimerTask> _tasks = new List<TimerTask>();

        internal static void Enqueue(TimerTask task)
        {
            _tasks.Add(task);
            _tasks.Sort((TimerTask t1, TimerTask t2) =>
            {
                if (t1.TrigTime == t2.TrigTime)
                {
                    return t1.Priority < t2.Priority ? -1 : 1;
                }
                return t1.TrigTime < t2.TrigTime ? -1 : 1;
            });
        }

        internal static TimerTask Dequeue()
        {
            if (_tasks.Count <= 0)
            {
                return null;
            }
            TimerTask task = _tasks[0];
            _tasks.RemoveAt(0);
            return task;
        }

        internal static TimerTask Peek()
        {
            if (_tasks.Count <= 0)
            {
                return null;
            }
            return _tasks[0];
        }
    }

}
