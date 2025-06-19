namespace GameBase.Tools
{
    public class SequentialBoolFixed
    {
        internal bool _currValue;
        internal bool _lastValue;
        public SequentialBoolFixed(bool initValue = false)
        {
            _currValue = initValue;
            _lastValue = initValue;
            SequentialMgr.Add(this);
        }
        ~SequentialBoolFixed()
        {
            SequentialMgr.Remove(this);
        }
        public static implicit operator bool(SequentialBoolFixed seq) => seq._currValue;
        public static explicit operator SequentialBoolFixed(bool val) => new SequentialBoolFixed(val);
        public void Set() => _currValue = true;
        public void Reset() => _currValue = false;
        public bool EdgeRising => !_lastValue && _currValue;
        public bool EdgeFalling => _lastValue && !_currValue;
    }
}
