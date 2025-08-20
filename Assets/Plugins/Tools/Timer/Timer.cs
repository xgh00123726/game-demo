using System;
using UnityEngine;

namespace GameBase.Tools
{
    /// <summary>
    /// 精确到帧的定时器
    /// </summary>
    public class Timer : MonoBehaviour
    {
        public int timerTaskNum = 0;
        public int timeLimitTaskNum = 0;
        /// <summary>
        /// 添加一个定时任务
        /// <list type="bullet">
        /// <item><param name="period"><paramref name="period"/>:任务的触发周期，如果为负数则每一帧都触发 </param></item>
        /// <item><param name="trigTimes"><paramref name="trigTimes"/>:任务的可触发次数，如果为负数则可以无限次触发</param></item>
        /// <item><param name="isImmediateTrig"><paramref name="isImmediateTrig"/>:true:立刻触发, false:等待一个周期后触发</param></item>
        /// <item><param name="callback"><paramref name="callback"/>:中断回调函数</param></item>
        /// </list></summary>
        public static void AddTask(float period, int trigTimes, bool isImmediateTrig, Action callback)
        {
            TimerTaskManager.AddTask(period, trigTimes, isImmediateTrig, callback);
        }

        /// <summary>
        /// 添加一个定时任务，无限次触发，第一次触发在添加任务的一个周期后
        /// <list type="bullet">
        /// <item><param name="period"><paramref name="period"/>:任务的触发周期，如果为负数则每一帧都触发 </param></item>
        /// <item><param name="callback"><paramref name="callback"/>:中断回调函数</param></item>
        /// </list></summary>
        public static void AddLoop(float period, Action callback)
        {
            TimerTaskManager.AddTask(period, callback);
        }

        /// <summary>
        /// 添加一个定时任务, 触发一次
        /// <list type="bullet">
        /// <item><param name="delay"><paramref name="delay"/>:触发延迟</param></item>
        /// <item><param name="callback"><paramref name="callback"/>:中断回调函数</param></item>
        /// </list></summary>
        public static void AddTask(float delay, Action callback)
        {
            TimerTaskManager.AddTask(delay, 1, false, callback);
        }
        /// <summary>
        /// 执行指定时间的回调
        /// <list type="bullet">
        /// <item><param name="beginTime"><paramref name="beginTime"/>:开始时刻</param></item>
        /// <item><param name="duration"><paramref name="duration"/>:持续时间</param></item>
        /// <item><param name="callback"><paramref name="callback"/>:回调</param></item>
        /// </list></summary>
        public static void DO(float beginTime, float duration, Action<float> callback)
        {
            TimeLimitTaskManager.AddTask(beginTime, duration, callback);
        }

        /// <summary>
        /// 立刻执行指定时间的回调
        /// <list type="bullet">
        /// <item><param name="duration"><paramref name="duration"/>:持续时间</param></item>
        /// <item><param name="callback"><paramref name="duration"/>:回调</param></item>
        /// </list></summary>
        public static void DO(float duration, Action<float> callback)
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
