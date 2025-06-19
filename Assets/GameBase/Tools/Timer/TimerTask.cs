using UnityEngine;
using System;

namespace GameBase.Tools
{
    internal class TimerTask
    {
        public TimerTask(float period, int trigTimes, bool isImmediateTrig, Action callback)
        {
            _period = period;
            _trigTimesRemain = trigTimes;
            _callback = callback;
            _priority = 0;
            if (isImmediateTrig)
            {
                _trigTime = Time.time;
            }
            else
            {
                _trigTime = Time.time + period;
            }
        }

        private float _period;           // 周期
        private float _trigTime;         // 触发时间
        private int _trigTimesRemain;          // 触发次数，负数时无限触发
        private int _priority;
        internal Action _callback;       // 中断函数

        internal float TrigTime { get => _trigTime; }
        internal int Priority { get => _priority; }
        internal bool IsInterruptable { get => _trigTimesRemain != 0; }
        internal bool IsInterrupt { get => Time.time > _trigTime; }
        internal void Interrupt()
        {
            if (_trigTimesRemain > 0)
            {
                _trigTimesRemain--;
            }
            _trigTime += _period;
        }
    }

}