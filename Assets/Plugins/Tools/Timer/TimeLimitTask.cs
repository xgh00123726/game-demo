using System;
using UnityEngine;

namespace GameBase.Tools
{
    public class TimeLimitTask
    {
        private float _beginTime = 0f;
        private float _endTime = 0f;
        private Action<float> _callback;

        internal TimeLimitTask(float beginTime, float duration, Action<float> callback)
        {
            _beginTime = beginTime;
            _endTime = beginTime + duration;
            _callback = callback;
        }

        internal bool BeginTrig => Time.time > _beginTime;
        internal bool EndTrig => Time.time > _endTime;

        internal void Update()
        {
            _callback?.Invoke(Time.time -  _beginTime);
        }

    }
}
