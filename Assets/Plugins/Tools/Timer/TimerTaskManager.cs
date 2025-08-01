using System;
using System.Collections.Generic;

namespace GameBase.Tools
{
    internal class TimerTaskManager
    {
        private static List<Action> _exectableTask = new List<Action>();
        private static TimerTask _taskTemp = null;

        internal static void ExecTasks()
        {
            // 查看定时器优先队列头部
            _taskTemp = TimerTaskQueue.Peek();
            // 如果头部存在且已经触发中断
            if (_taskTemp?.IsInterrupt == true)
            {
                // 任务出队
                TimerTaskQueue.Dequeue();

                // 任务触发中断：将任务的回调函数添加到待执行队列中
                _taskTemp.Interrupt();
                _exectableTask.Add(_taskTemp._callback);

                // 如果任务还可以继续中断
                if (_taskTemp.IsInterruptable)
                {
                    // 任务重新入队
                    TimerTaskQueue.Enqueue(_taskTemp);
                }
            }

            // 将待执行中断全部执行
            foreach (var task in  _exectableTask)
            {
                task();
            }
            _exectableTask.Clear();
        }

        internal static void AddTask(float period, int trigTimes, bool isImmediateTrig, Action callback)
        {
            TimerTaskQueue.Enqueue(new TimerTask(period, trigTimes, isImmediateTrig, callback));
        }

        internal static void AddTask(float period, Action callback)
        {
            AddTask(period, -1, false, callback);
        }
    }
}
