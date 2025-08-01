using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameBase.Tools.TimeLimitTask;

namespace GameBase.Tools
{
    internal class TimeLimitTaskManager
    {
        internal static List<TimeLimitTask> _tasks = new List<TimeLimitTask>();
        private static List<TimeLimitTask> _tasksAdd = new List<TimeLimitTask>();
        private static List<TimeLimitTask> _tasksRemove = new List<TimeLimitTask>();

        internal static void AddTask(float beginTime, float duration, TimeLimitTaskDelegate callback)
        {
            _tasksAdd.Add(new TimeLimitTask(beginTime, duration, callback));
        }

        internal static void RemoveTask(TimeLimitTask task)
        {
            _tasksRemove.Add(task);
        }

        static internal void ExecTasks()
        {
            foreach (var task in _tasksAdd)
            {
                _tasks.Add(task);
            }
            _tasksAdd.Clear();

            foreach (var task in _tasks)
            {
                if (task.EndTrig)
                {
                    RemoveTask(task);
                    continue;
                }
                if (task.BeginTrig)
                {
                    task.Update();
                }
            }

            foreach (var task in _tasksRemove)
            {
                _tasks.Remove(task);
            }
            _tasksRemove.Clear();
        }
    }
}
