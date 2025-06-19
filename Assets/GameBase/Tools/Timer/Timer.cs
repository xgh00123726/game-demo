using System;
using UnityEngine;
using static GameBase.Tools.TimeLimitTask;

namespace GameBase.Tools
{
    // 精确到帧的定时器
    public class Timer : MonoBehaviour
    {
        public int timerTaskNum = 0;
        public int timeLimitTaskNum = 0;
        /* @FUNC : 添加一个定时任务
         * @PARAM period:          任务的触发周期，如果为负数则每一帧都触发 
         * @PARAM trigTimes:       任务的可触发次数，如果为负数则可以无限次触发
         * @PARAM isImmediateTrig: 立刻触发还是等待一个周期后触发
         * @PARAM callback:        中断回调函数
         */
        public static void AddTask(float period, int trigTimes, bool isImmediateTrig, Action callback)
        {
            TimerTaskManager.AddTask(period, trigTimes, isImmediateTrig, callback);
        }

        /* @FUNC : 添加一个定时任务，无限次触发，第一次触发在添加任务的一个周期后
         * @PARAM period:          任务的触发周期，如果为负数则每一帧都触发 
         * @PARAM callback:        中断回调函数
         */
        public static void AddLoop(float period, Action callback)
        {
            TimerTaskManager.AddTask(period, callback);
        }

        public static void AddTask(float delay, Action callback)
        {
            TimerTaskManager.AddTask(delay, 1, false, callback);
        }
        // 从beginTime开始，每一帧都执行一次callback，持续duration秒
        public static void DO(float beginTime, float duration, TimeLimitTaskDelegate callback)
        {
            TimeLimitTaskManager.AddTask(beginTime, duration, callback);
        }
        // 从当前开始，每一帧都执行一次callback，持续duration秒
        public static void DO(float duration, TimeLimitTaskDelegate callback)
        {
            DO(Time.time, duration, callback);
        }

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected void Update()
        {
            TimerTaskManager.ExecTasks();
            TimeLimitTaskManager.ExecTasks();
            timerTaskNum = TimerTaskQueue._tasks.Count;
            timeLimitTaskNum = TimeLimitTaskManager._tasks.Count;
        }
    }

}
