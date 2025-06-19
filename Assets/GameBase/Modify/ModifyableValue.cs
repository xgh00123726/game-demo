namespace GameBase.Modify
{
    public struct ModifyableValue<T>
    {
        public T Set;
        public T Value
        {
            get;
            internal set;
        }
        internal void Reset()
        {
            Value = Set;
        }
    }
}
