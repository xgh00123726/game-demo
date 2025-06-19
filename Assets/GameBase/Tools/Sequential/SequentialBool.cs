using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameBase.Tools
{
    public class SequentialBool
    {
        internal bool _currValue;
        internal bool _lastValue;
        public SequentialBool(bool initValue = false)
        {
            _currValue = initValue;
            _lastValue = initValue;
            SequentialMgr.Add(this);
        }
        ~SequentialBool()
        {
            SequentialMgr.Remove(this);
        }
        public static implicit operator bool(SequentialBool seq) => seq._currValue;
        public static explicit operator SequentialBool(bool val) => new SequentialBool(val);
        public void Set() => _currValue = true;
        public void Reset() => _currValue = false;
        public bool EdgeRising => !_lastValue && _currValue;
        public bool EdgeFalling => _lastValue && !_currValue;
    }
}
