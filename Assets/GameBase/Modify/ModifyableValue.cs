using UnityEngine;

namespace GameBase.Modify
{
    public struct ModifyableValue<T>
    {
        public T Set
        {
            get;
            internal set;
        }
        public T Value
        {
            get;
            internal set;
        }
        internal void Reset()
        {
            Value = Set;
        }

        public static implicit operator T(ModifyableValue<T> value) => value.Value;
        public static implicit operator ModifyableValue<T>(T value) => new ModifyableValue<T> { Set = value, Value = value };
    }
}
